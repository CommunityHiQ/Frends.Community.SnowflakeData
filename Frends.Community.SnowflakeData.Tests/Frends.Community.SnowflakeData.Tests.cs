using NUnit.Framework;
using System;


namespace Frends.Community.SnowflakeData.Tests
{
    [TestFixture]
    class TestClass
    {
        /// <summary>
        /// You need to run Frends.Community.SnowflakeData.SetPaswordsEnv.ps1 before running unit test, or some other way set environment variables e.g. with GitHub Secrets.
        /// </summary>
        [Test]
        public void Jaaha()
        {
            var input = new QueryParameters
            {
                ConnectionString = "",
                Query = "select * from TEST where TESTISARAKE1 > :mika",
                Parameters = new MyParameter[] { new MyParameter{
                Name = "mika",
                Value = 1
                } }
            };
        
            var options = new Options
            {
                CommandTimeoutSeconds= 30,
                IsolationLevel = null
            };

            var ret = Flake.ExecuteQuery(input, options, new System.Threading.CancellationToken());
            var rs = ret.Result;

            Assert.IsNotNull(rs);
        }
    }
}
