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

        public async Task<int> CreateAsync(TransactionDTO transaction)
        {
            return await PostAsync<int>("transaction", transaction);
        }
    }
}
