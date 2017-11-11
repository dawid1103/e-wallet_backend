using Dapper;
using EwalletService.DataAccessLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Data.SqlClient;
using System.Net.Http;
using Xunit;
using EwalletService;
using EwalletCommon.Endpoints;

namespace EwalletTests.Common
{
    [Collection(nameof(EwalletService))]
    public abstract class TestBase
    {
        protected readonly TestServer _server;
        protected readonly Ewallet _ewalletService;

        public TestBase()
        {
            IWebHostBuilder builder = new WebHostBuilder().UseStartup<Startup>();
            _server = new TestServer(builder);

            ClearDatabase();

            HttpClient client = _server.CreateClient();
            _ewalletService = new Ewallet(client);
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
