using System.Net.Http;
using System.Threading.Tasks;

namespace EwalletCommon.Endpoints
{
    public class AccountBalanceEndpoint : ServiceEndpoint
    {
        public AccountBalanceEndpoint(string url) : base(url)
        {
        }

        public AccountBalanceEndpoint(HttpClient client) : base(client)
        {
        }

        /// <summary>
        /// Returns account balance for selected user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Amount of balance</returns>
        public async Task<decimal> GetAsync(int userId)
        {
            return await base.GetAsync<decimal>($"accountBalance/user/{userId}");
        }
    }
}
