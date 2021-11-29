using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Snowflake.Data.Client;
using System.Data;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


#pragma warning disable 1591

namespace Frends.Community.SnowflakeData
{
    public static class Flake
    {
        /// <summary>
        /// Execute a sql query. can return more than one resultset ( multiple queries in one input )
        /// </summary>
        /// <param name="input">Input parameters</param>
        /// <param name="options">Optional parameters with default values</param>
        /// <param name="cancellationToken"> CancellationToken, maybe it works with long queries</param>
        /// <returns>JToken : an Array if there is only one resultset or Array of Arrays if there are multiple resultsets</returns>
        public static async Task<object> ExecuteQuery([PropertyTab] QueryParameters input, [PropertyTab] Options options, CancellationToken cancellationToken)
        {
            return await GetSqlCommandResult(input.Query, input.ConnectionString, input.Parameters , options,  CommandType.Text, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a stored procedure. can return more than one resultset.
        /// </summary>
        /// <param name="input">Input parameters</param>
        /// <param name="options">Optional parameters with default values</param>
        /// <param name="cancellationToken"> CancellationToken, maybe it works with long queries</param>
        /// <returns>JToken : an Array if there is only one resultset or Array of Arrays if there are multiple resultsets</returns>
        public static async Task<object> ExecuteProcedure([PropertyTab] ProcedureParameters input, [PropertyTab] Options options, CancellationToken cancellationToken)
        {
            return await GetSqlCommandResult(input.Execute, input.ConnectionString, input.Parameters, options, CommandType.StoredProcedure, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Create a query for a batch operation like insert. 
        /// </summary>
        /// <param name="input">Input parameters</param>
        /// <param name="options">Optional parameters with default values</param>
        /// <param name="cancellationToken"> CancellationToken, maybe it works with long queries</param>
        /// <returns>Number of affected rows</returns>
        public static async Task<int> BatchOperation([PropertyTab]BatchOperationParameters input, [PropertyTab]Options options, CancellationToken cancellationToken)
        {
            int affected = 0;
            using (var sqlConnection = new SnowflakeDbConnection())
            {
                sqlConnection.ConnectionString = input.ConnectionString;
                await sqlConnection.OpenAsync(cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                var obj = JsonConvert.DeserializeObject<ExpandoObject[]>(input.InputJson, new Newtonsoft.Json.Converters.ExpandoObjectConverter());

                 using(var transaction = options.IsolationLevel == null ?
                    null :
                        options.IsolationLevel == IsolationLevel.Unspecified ?
                        sqlConnection.BeginTransaction()
                        : sqlConnection.BeginTransaction((IsolationLevel) options.IsolationLevel)
                    )
                {
                    
                    var command = sqlConnection.CreateCommand();
                    command.CommandType = input.StoredProcedure ? CommandType.StoredProcedure : CommandType.Text;
                    command.CommandText = input.Query;
                    foreach(ExpandoObject o in obj){
                        command.Parameters.Clear();
                        foreach (var parameter in o)
                        {
                            var par = new SnowflakeDbParameter();
                            par.ParameterName = parameter.Key;
                            par.Value = parameter.Value;
                            par.DbType = Util.DbTypeFor(parameter.Value);
                            command.Parameters.Add( par);
                        }
                        var result = await command.ExecuteNonQueryAsync();
                        affected += result;
                    }
                    if(transaction!=null){
                        transaction.Commit();
                    }
                }
            }
            return affected;
        }


        private static async Task<JToken> GetSqlCommandResult(string query, string connectionString, IEnumerable<MyParameter> parameters, Options options, CommandType commandType, CancellationToken cancellationToken)
        {
            using (var sqlConnection = new SnowflakeDbConnection())
            {
                sqlConnection.ConnectionString = connectionString;
                await sqlConnection.OpenAsync(cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                using(var transaction = options.IsolationLevel == null ?
                    null :
                        options.IsolationLevel == IsolationLevel.Unspecified ?
                        sqlConnection.BeginTransaction()
                        : sqlConnection.BeginTransaction((IsolationLevel) options.IsolationLevel)
                    )
                {

                

                    using (var command = sqlConnection.CreateCommand())
                    {
                        command.CommandText = query;
                        command.CommandTimeout = options.CommandTimeoutSeconds;
                        foreach (var parameter in parameters)
                        {
                            var par = new SnowflakeDbParameter();
                            par.ParameterName = parameter.Name;
                            par.Value = parameter.Value;
                            par.DbType = Util.DbTypeFor(parameter.Value);
                            command.Parameters.Add( par);
                        }
                        command.CommandType = commandType;
                        var result = await command.ExecuteReaderAsync(cancellationToken);
                        var rlist = new List<DataTable>();
                        do
                        {
                            var table = new DataTable();
                            table.Load(result);
                            rlist.Add(table);
                        } while (result.NextResult());

                        if (rlist.Count == 1) {
                            return JToken.FromObject(rlist[0]);
                        } else
                        {
                            return JToken.FromObject(rlist);
                        }
                    }
                }
            }
        }
    }
}
