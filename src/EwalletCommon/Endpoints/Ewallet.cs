using System.Net.Http;

namespace EwalletCommon.Endpoints
{
    public class Ewallet : ServiceEndpoint
    {
        public TransactionEndpoint Transaction { get; }
        public CategoryEndpoint Category { get; }
        public UserEndpoint User { get; }

        public Ewallet(string baseUrl) : base(baseUrl)
        {
            Transaction = new TransactionEndpoint(_httpClient);
            Category = new CategoryEndpoint(_httpClient);
            User = new UserEndpoint(_httpClient);
        }

        public Ewallet(HttpClient client) : base(client)
        {
            Transaction = new TransactionEndpoint(_httpClient);
            Category = new CategoryEndpoint(_httpClient);
            User = new UserEndpoint(_httpClient);
        }
    }
}
