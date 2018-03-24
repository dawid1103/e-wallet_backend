using Dapper;
using EwalletService.DataAccessLayer;
using System.Threading.Tasks;

namespace EwalletService.Repository
{
    public interface IAccountBalanceRepository
    {
        Task<decimal> GetBalace(int userId);
        Task UpdateBalance(int userId, decimal amount);
    }

    internal class AccountBalanceRepository : IAccountBalanceRepository
    {
        private readonly IDatabaseSession dbSession;

        public AccountBalanceRepository(IDatabaseSession dbSession)
        {
            this.dbSession = dbSession;
        }

        public async Task<decimal> GetBalace(int userId)
        {
            decimal? balance = await dbSession.Connection.QuerySingleOrDefaultAsync<decimal?>("SELECT amount FROM [AccountBalance] WHERE [userId]=@userId;", new
            {
                @userId = userId
            });

            if (balance == null)
            {
                dbSession.Connection.Execute("INSERT INTO [AccountBalance] (userId) VALUES (@userId);", new
                {
                    @userId = userId
                });
            }

            return balance ?? 0.00m;
        }

        public async Task UpdateBalance(int userId, decimal amount)
        {
            decimal balance = await GetBalace(userId) + amount;

            dbSession.Connection.Execute("UPDATE [AccountBalance] SET [amount]=@amount WHERE [userId]=@userId", new
            {
                @amount = balance,
                @userId = userId
            });
        }
    }
}
