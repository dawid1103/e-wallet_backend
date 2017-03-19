using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EwalletCommon.Endpoints
{
    public class WalletService : ServiceEndpoint
    {
        public WalletService(string baseUrl) : base(baseUrl)
        {
            Transaction = new TransactionEndpoint(_httpClient);
        }

        public WalletService(HttpClient client) : base(client)
        {
            Transaction = new TransactionEndpoint(_httpClient);
        }

        public TransactionEndpoint Transaction { get; }
    }
}
