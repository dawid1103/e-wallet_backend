using EwalletCommon.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EwalletCommon.Endpoints
{
    public class ScheduledTransactionEndpoint : ServiceEndpoint
    {
        public ScheduledTransactionEndpoint(string url) : base(url)
        {
        }

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

        /// <summary>
        /// Delete scheduled transaction of given category id
        /// </summary>
        /// <param name="id">Transaction id</param>
        public async Task DeleteAsync(int id)
        {
            await DeleteAsync($"scheduledTransaction/{id}");
        }

        /// <summary>
        /// Returns ScheduledTransactionDTO object of category with gicen transaction id
        /// </summary>
        /// <param name="id">Tategorytransaction id</param>
        /// <returns>ScheduledTransactionDTO object</returns>
        public async Task<ScheduledTransactionDTO> GetAsync(int id)
        {
            return await base.GetAsync<ScheduledTransactionDTO>($"scheduledTransaction/{id}");
        }

        /// <summary>
        /// Returns all scheduled transactions
        /// </summary>
        /// <returns>IEnumerable<ScheduledTransactionDTO></returns>
        public async Task<IEnumerable<ScheduledTransactionDTO>> GetAllAsync()
        {
            return await base.GetAsync<IEnumerable<ScheduledTransactionDTO>>("scheduledTransaction");
        }

        /// <summary>
        /// Update given scheduled transaction
        /// </summary>
        /// <param name="category">ScheduledTransactionDTO object</param>
        public async Task UpdateAsync(ScheduledTransactionDTO transaction)
        {
            await base.PutAsync("scheduledTransaction", transaction);
        }

        /// <summary>
        /// Get scheduled transactions by user
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>List of user scheduled transactions</returns>
        public async Task<IEnumerable<ScheduledTransactionDTO>> GetAllByUserIdAsync(int id)
        {
            return await base.GetAsync<IEnumerable<ScheduledTransactionDTO>>($"scheduledTransaction/user/{id}");
        }
    }
}
