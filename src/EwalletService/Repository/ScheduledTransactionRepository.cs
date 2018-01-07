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
        Task<IEnumerable<ScheduledTransactionDTO>> GetAllByUserIdAsync(int id);
    }

    public class ScheduledTransactionRepository : Repository, IScheduledTransactionRepository
    {
        public ScheduledTransactionRepository(IDatabaseSession dbSession) : base(dbSession)
        {
        }

        public async Task<int> CreateAsync(ScheduledTransactionDTO transaction)
        {
            IEnumerable<int> result = await base.LoadByStorageProcedureAsync<int>("dbo.ScheduledTransactionCreate", new
            {
                title = transaction.Title,
                description = transaction.Description,
                price = transaction.Price,
                categoryId = transaction.CategoryId,
                userId = transaction.UserId,
                repeatDay = transaction.RepeatDay,
                repeatCount = transaction.RepeatCount,
                repeatMode = transaction.RepeatMode
            });

            return result.FirstOrDefault();
        }

        public async Task DeleteAsync(int id)
        {
            await base.ExecuteStorageProcedureAsync("dbo.ScheduledTransactionDelete", new
            {
                id = id
            });
        }

        public async Task EditAsync(ScheduledTransactionDTO transaction)
        {
            await base.ExecuteStorageProcedureAsync("dbo.ScheduledTransactionUpdate", new
            {
                id = transaction.Id,
                title = transaction.Title,
                description = transaction.Description,
                price = transaction.Price,
                categoryId = transaction.CategoryId,
                repeatDay = transaction.RepeatDay,
                repeatCount = transaction.RepeatCount,
                repeatMode = transaction.RepeatMode
            });
        }

        public async Task<IEnumerable<ScheduledTransactionDTO>> GetAllAsync()
        {
            return await base.LoadByStorageProcedureAsync<ScheduledTransactionDTO>("dbo.ScheduledTransactionGetAll", null);
        }

        public async Task<IEnumerable<ScheduledTransactionDTO>> GetAllByUserIdAsync(int id)
        {
            return await base.LoadByStorageProcedureAsync<ScheduledTransactionDTO>("dbo.ScheduledTransactionGetAllByUserId", new
            {
                id = id
            });

        }

        public async Task<IEnumerable<ScheduledTransactionDTO>> GetAllIncomingAsync(DateTime currentDate)
        {
            //return transactions.Where(t => t.RepeatDay <= currentDate && t.IsCompleted == false).Select(t => t);
            return await base.LoadByStorageProcedureAsync<ScheduledTransactionDTO>("dbo.ScheduledTransactionGetAllIncoming", new
            {
                date = currentDate,
            });
        }

        public async Task<ScheduledTransactionDTO> GetAsync(int id)
        {
            IEnumerable<ScheduledTransactionDTO> result = await base.LoadByStorageProcedureAsync<ScheduledTransactionDTO>("dbo.ScheduledTransactionGet", new
            {
                id = id
            });

            return result.FirstOrDefault();
        }

        public async Task SetNextIntervalAsync(int id, DateTime nextDate, int repeatCount)
        {
            await base.ExecuteStorageProcedureAsync("dbo.ScheduledTransactionNextCreateDate", new
            {
                id = id,
                repeatDay = nextDate,
                repeatCount = repeatCount,
            });
        }
    }
}
