#pragma warning disable 1591

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Snowflake.Data.Client;

namespace Frends.Community.SnowflakeData
{
    /// <summary>
    /// Class to pass on the parameters to the queries
    /// </summary>
    public class MyParameter {
        /// <summary>
        /// name of the parameter
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        public string Name { get; set; }

        /// <summary>
        /// Value of the parameter
        /// </summary>
        /// 
        public object Value { get; set; }


    }


    /// <summary>
    /// Parameters class usually contains parameters that are required.
    /// </summary>
    public class QueryParameters
    {
        /// <summary>
        /// Query text.
        /// </summary>
        [DisplayFormat(DataFormatString = "Sql")]
        public string Query { get; set; }
        /// <summary>
        /// Parameters for query.
        /// </summary>
        public MyParameter[] Parameters { get; set; }

        /// <summary>
        /// Connection string
        /// </summary>
        [PasswordPropertyText]
        [DefaultValue("\"ACCOUNT=;DB=;HOST=;PASSWORD=;ROLE=;SCHEMA=;USER=\"")]
        public string ConnectionString { get; set; }
    }
    public class ProcedureParameters
    {
        /// <summary>
        /// Query text.
        /// </summary>
        [DisplayFormat(DataFormatString = "Sql")]
        public string Execute { get; set; }
        /// <summary>
        /// Procedure to execute .
        /// </summary>
        public MyParameter[] Parameters { get; set; }

        /// <summary>
        /// Connection string
        /// </summary>
        [PasswordPropertyText]
        [DefaultValue("\"ACCOUNT=;DB=;HOST=;PASSWORD=;ROLE=;SCHEMA=;USER=\"")]
        public string ConnectionString { get; set; }
    }

    public class BatchOperationParameters
    {
        /// <summary>
        /// Query string for batch operation.
        /// </summary>
        [DisplayFormat(DataFormatString = "Sql")]
        [DefaultValue("insert into MyTable(ID,NAME) VALUES (:Id, :FirstName)")]
        public string Query { get; set; }

        /// <summary>
        /// When specified, the query is treated as a stored-procedure
        /// </summary>
        public bool StoredProcedure { get; set; }

        /// <summary>
        /// Input json for batch operation. Needs to be a Json array.
        /// </summary>
        [DisplayFormat(DataFormatString = "Json")]
        [DefaultValue("[{\"Id\":15,\"FirstName\":\"Foo\"},{\"Id\":20,\"FirstName\":\"Bar\"}]")]
        public string InputJson { get; set; }

        /// <summary>
        /// Connection string
        /// </summary>
        [PasswordPropertyText]
        [DefaultValue("\"ACCOUNT=;DB=;HOST=;PASSWORD=;ROLE=;SCHEMA=;USER=\"")]
        public string ConnectionString { get; set; }
    }


    public class BulkInsertParameters
    {
        /// <summary>
        /// Json Array of objects. All object property names need to match with the destination table column names.
        /// </summary>
        [DisplayFormat(DataFormatString = "Json")]
        [DefaultValue("[{\"Column1\":\"Value1\", \"Column2\":15},{\"Column1\":\"Value2\", \"Column2\":30}]")]
        public string InputData { get; set; }

        /// <summary>
        /// Destination table name.
        /// </summary>
        [DefaultValue("\"TestTable\"")]
        public string TableName { get; set; }

        /// <summary>
        /// Connection string
        /// </summary>
        [PasswordPropertyText]
        [DefaultValue("\"ACCOUNT=;DB=;HOST=;PASSWORD=;ROLE=;SCHEMA=;USER=\"")]
        public string ConnectionString { get; set; }
    }


   public class BulkInsertOptions
    {
        [DefaultValue(60)]
        public int CommandTimeoutSeconds { get; set; }
        /// <summary>
        /// When specified, cause the server to fire the insert triggers for the rows being inserted into the database.
        /// </summary>
        public bool FireTriggers { get; set; }
        /// <summary>
        /// Preserve source identity values. When not specified, identity values are assigned by the destination.
        /// </summary>
        public bool KeepIdentity { get; set; }

        /// <summary>
        /// If the input properties have empty values i.e. "", the values will be converted to null if this parameter is set to true.
        /// </summary>
        public bool ConvertEmptyPropertyValuesToNull { get; set; }
        /// <summary>
        /// Transactions specify an isolation level that defines the degree to which one transaction must be isolated from resource or data modifications made by other transactions. Default is Serializable.
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }
    }


    /// <summary>
    /// Options class provides additional optional parameters.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Command timeout in seconds
        /// </summary>
        [DefaultValue(60)]
        public int CommandTimeoutSeconds { get; set; }

        /// <summary>
        /// Transactions specify an isolation level that defines the degree to which one transaction must be isolated from resource or data modifications made by other transactions. Default is Serializable.
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }
    }
    
}
