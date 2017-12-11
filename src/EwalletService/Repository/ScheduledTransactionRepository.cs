using EwalletCommon.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EwalletService.DataAccessLayer;
using System.Linq;

namespace EwalletService.Repository
{
    public interface IScheduledTransactionRepository : IRepository<ScheduledTransactionDTO>
    {
        Task<IEnumerable<ScheduledTransactionDTO>> GetAllIncomingAsync(DateTime currentDate);
        Task SetNextIntervalAsync(int id, DateTime nextRepeatDate, int repeatCount);
    }

    public class ScheduledTransactionRepository : Repository, IScheduledTransactionRepository
    {
        private IEnumerable<ScheduledTransactionDTO> transactions;

        public ScheduledTransactionRepository(IDatabaseSession dbSession) : base(dbSession)
        {
            transactions = new List<ScheduledTransactionDTO>()
            {
                new ScheduledTransactionDTO()
                {
                    Id=2,
                    Title="#1 transaction",
                    Description = "#1 Description",
                    Price = 2.99M,
                    RepeatCount=2,
                    RepeatMode=RepeatMode.Weekly,
                    StartDay = DateTime.Now.Date,
                    NextCreateDay = DateTime.Now.Date.AddDays(-1),
                    UserId=1,
                    CategoryId =2
                },
                new ScheduledTransactionDTO()
                {
                    Id=2,
                    Title="#2 transaction",
                    Description = "#2 Description",
                    Price = 2.99M,
                    RepeatCount=0,
                    RepeatMode=RepeatMode.Daily,
                    StartDay = DateTime.Now.Date,
                    NextCreateDay = DateTime.Now.Date.AddDays(-1),
                    UserId=1,
                    CategoryId =2
                },
                new ScheduledTransactionDTO()
                {
                    Id=4,
                    Title="#4 transaction",
                    Description = "#4 Description",
                    Price = 2.99M,
                    RepeatCount=1,
                    RepeatMode=RepeatMode.Daily,
                    StartDay = DateTime.Now.Date,
                    NextCreateDay = DateTime.Now.Date.AddDays(-1),
                    UserId=1,
                    CategoryId =2
                },
                new ScheduledTransactionDTO()
                {
                    Id=3,
                    Title="#3 transaction",
                    Description = "#3 Description",
                    Price = 12.99M,
                    RepeatCount=5,
                    RepeatMode=RepeatMode.Daily,
                    StartDay = DateTime.Now.Date,
                    NextCreateDay = DateTime.Now.Date.AddDays(-1),
                    UserId=1,
                    CategoryId =2
                }
            };
        }

        public async Task<int> CreateAsync(ScheduledTransactionDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task EditAsync(ScheduledTransactionDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ScheduledTransactionDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ScheduledTransactionDTO>> GetAllIncomingAsync(DateTime currentDate)
        {
            return transactions.Where(t => t.NextCreateDay <= currentDate && t.IsCompleted == false).Select(t => t);
        }

        public async Task<ScheduledTransactionDTO> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task SetNextIntervalAsync(int id, DateTime nextDate, int repeatCount)
        {
            var tra = transactions.First(t => t.Id == id);
            tra.NextCreateDay = nextDate;
            tra.RepeatCount = repeatCount;
        }
    }
}
