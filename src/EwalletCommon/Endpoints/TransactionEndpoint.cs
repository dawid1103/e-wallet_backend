using EwalletCommon.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EwalletCommon.Endpoints
{
    public class TransactionEndpoint : ServiceEndpoint
    {
        public TransactionEndpoint(string url) : base(url)
        {
        }

        public TransactionEndpoint(HttpClient client) : base(client)
        {
        }

        /// <summary>
        /// Create transaction
        /// </summary>
        /// <param name="transaction">TransactionDTO object</param>
        /// <returns>Transaction id of created</returns>
        public async Task<int> CreateAsync(TransactionDTO transaction)
        {
            return await PostAsync<int>("transaction", transaction);
        }

        /// <summary>
        /// Delete transaction of given category id
        /// </summary>
        /// <param name="id">Transaction id</param>
        public async Task DeleteAsync(int id)
        {
            await DeleteAsync($"transaction/{id}");
        }

        /// <summary>
        /// Returns TransactionDTO object of category with gicen transaction id
        /// </summary>
        /// <param name="id">Tategorytransaction id</param>
        /// <returns>TransactionDTO object</returns>
        public async Task<TransactionDTO> GetAsync(int id)
        {
            return await base.GetAsync<TransactionDTO>($"transaction/{id}");
        }

        /// <summary>
        /// Returns all user transactions
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List of user transactions</returns>
        public async Task<IEnumerable<TransactionDTO>> GetAllAsync(int userId)
        {
            return await base.GetAsync<IEnumerable<TransactionDTO>>($"transaction/user/{userId}");
        }

        /// <summary>
        /// Update given transaction
        /// </summary>
        /// <param name="category">TransactionDTO object</param>
        public async Task UpdateAsync(TransactionDTO transaction)
        {
            await base.PutAsync("transaction", transaction);
        }

        /// <summary>
        /// Get summary of all transactions from current month
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Dictionary with transactions grouped by category</returns>
        public async Task<Dictionary<string, IEnumerable<TransactionDTO>>> GetSummary(int userId)
        {
            return await base.GetAsync<Dictionary<string, IEnumerable<TransactionDTO>>>($"transaction/summary/{userId}");
        }
    }
}
