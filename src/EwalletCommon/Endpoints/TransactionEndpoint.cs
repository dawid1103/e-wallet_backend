using EwalletCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EwalletCommon.Endpoints
{
    public class TransactionEndpoint : ServiceEndpoint
    {
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
        /// Returns all transactions
        /// </summary>
        /// <returns>IEnumerable<TransactionDTO></returns>
        public async Task<IEnumerable<TransactionDTO>> GetAllAsync()
        {
            return await base.GetAsync<IEnumerable<TransactionDTO>>("transaction");
        }

        /// <summary>
        /// Update given transaction
        /// </summary>
        /// <param name="category">TransactionDTO object</param>
        public async Task UpdateAsync(TransactionDTO transaction)
        {
            await base.PutAsync("transaction", transaction);
        }
    }
}
