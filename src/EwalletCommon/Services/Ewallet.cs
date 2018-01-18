using System.Net.Http;
using EwalletCommon.Endpoints;

namespace EwalletCommon.Services
{
    public class Ewallet
    {
        public TransactionEndpoint Transaction { get; }
        public ScheduledTransactionEndpoint ScheduledTransaction { get; }
        public CategoryEndpoint Category { get; }
        public UserEndpoint User { get; }

        public Ewallet(string baseUrl)
        {
            Transaction = new TransactionEndpoint(baseUrl);
            ScheduledTransaction = new ScheduledTransactionEndpoint(baseUrl);
            Category = new CategoryEndpoint(baseUrl);
            User = new UserEndpoint(baseUrl);
        }

        public Ewallet(HttpClient client)
        {
            Transaction = new TransactionEndpoint(client);
            ScheduledTransaction = new ScheduledTransactionEndpoint(client);
            Category = new CategoryEndpoint(client);
            User = new UserEndpoint(client);
        }
    }
}
