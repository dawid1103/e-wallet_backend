using Dapper;
using EwalletService.DataAccessLayer;

namespace EwalletService.Repository
{
    internal interface IAccountBalanceRepository
    {
        decimal GetBalace(int userId);
        void UpdateBalance(int userId, decimal amount);
    }

    public class AccountBalanceRepository : IAccountBalanceRepository
    {
        private readonly IDatabaseSession dbSession;

        public AccountBalanceRepository(IDatabaseSession dbSession)
        {
            this.dbSession = dbSession;
        }

        public decimal GetBalace(int userId)
        {
            decimal? balance = dbSession.Connection.QuerySingleOrDefault<decimal?>("SELECT amount FROM [AccountBalance] WHERE [userId]=@userId;", new
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

        public void UpdateBalance(int userId, decimal amount)
        {
            decimal balance = GetBalace(userId) + amount;

            dbSession.Connection.Execute("UPDATE [AccountBalance] SET [amount]=@amount WHERE [userId]=@userId", new
            {
                @amount = balance,
                @userId = userId
            });
        }
    }
}
