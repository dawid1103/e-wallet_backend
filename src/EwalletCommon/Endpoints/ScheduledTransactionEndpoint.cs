using EwalletCommon.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace EwalletCommon.Endpoints
{
    public class ScheduledTransactionEndpoint : ServiceEndpoint
    {
        public ScheduledTransactionEndpoint(HttpClient client) : base(client)
        {
        }

        /// <summary>
        /// Create scheduled transaction
        /// </summary>
        /// <param name="transaction">ScheduledTransactionDTO object</param>
        /// <returns>Scheduled transaction id of created</returns>
        public async Task<int> CreateAsync(ScheduledTransactionDTO transaction)
        {
            return await PostAsync<int>("scheduledTransaction", transaction);
        }
    }
}
