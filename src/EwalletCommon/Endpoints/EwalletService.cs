using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EwalletCommon.Endpoints
{
    public class EwalletService : ServiceEndpoint
    {
        public TransactionEndpoint Transaction { get; }
        public CategoryEndpoint Category { get; }
        public UserEndpoint User { get; }

        public EwalletService(string baseUrl) : base(baseUrl)
        {
            Transaction = new TransactionEndpoint(_httpClient);
            Category = new CategoryEndpoint(_httpClient);
            User = new UserEndpoint(_httpClient);
        }

        public EwalletService(HttpClient client) : base(client)
        {
            Transaction = new TransactionEndpoint(_httpClient);
            Category = new CategoryEndpoint(_httpClient);
            User = new UserEndpoint(_httpClient);
        }

    }
}
