using Dapper;
using EwalletCommon.Endpoints;
using EwalletServices;
using EwalletServices.DataAccessLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace EwalletTests.Common
{
    [Collection(nameof(EwalletService))]
    public abstract class TestBase
    {
        protected readonly TestServer _server;
        protected readonly EwalletService _ewalletService;

        public TestBase()
        {
            IWebHostBuilder builder = new WebHostBuilder().UseStartup<Startup>();
            _server = new TestServer(builder);

            ClearDatabase();

            HttpClient client = _server.CreateClient();
            _ewalletService = new EwalletService(client);
        }

        protected void ClearDatabase()
        {
            DatabaseConfig db = (DatabaseConfig)_server.Host.Services.GetService(typeof(DatabaseConfig));
            using (var connection = new SqlConnection(db.ConnectionString))
            {
                connection.Execute("dbo.ClearDatabase", commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
