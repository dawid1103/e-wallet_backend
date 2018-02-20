using Dapper;
using EwalletCommon.Models;
using EwalletCommon.Services;
using EwalletService;
using EwalletService.DataAccessLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace EwalletTests.Common
{
    [Collection(nameof(EwalletService))]
    public abstract class TestBase
    {
        protected readonly TestServer server;
        protected readonly Ewallet ewalletService;

        public TestBase()
        {
            IWebHostBuilder builder = new WebHostBuilder().UseStartup<Startup>();
            server = new TestServer(builder);

            // ClearDatabase();

            HttpClient client = server.CreateClient();
            ewalletService = new Ewallet(client);
        }

        protected void ClearDatabase()
        {
            DatabaseConfig db = (DatabaseConfig)server.Host.Services.GetService(typeof(DatabaseConfig));
            using (var connection = new SqlConnection(db.ConnectionString))
            {
                connection.Execute("dbo.ClearDatabase", commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        protected int CreateUser()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int userId = ewalletService.User.CreateAsync(user).Result;
            return userId;
        }
    }
}
