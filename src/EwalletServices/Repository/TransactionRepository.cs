using EwalletCommon.Models;
using EwalletServices.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletServices.Repository
{
    public interface ITransactionRepository : IRepository<TransactionDTO>
    {
    }
    public class TransactionRepository : Repository, ITransactionRepository
    {
        public TransactionRepository(IDatabaseSession dbSession) : base(dbSession) { }

        public async Task<int> AddAsync(TransactionDTO transaction)
        {
            IEnumerable<int> results = await base.LoadByStorageProcedureAsync<int>("dbo.TransactionCreate", new
            {
                title = transaction.Title,
                description = transaction.Description,
                categoryId = transaction.CategoryId
            });

            return results.FirstOrDefault();
        }

        public async Task DeleteAsync(int id)
        {
            await base.ExecuteStorageProcedureAsync("dbo.TransactionDelete", new
            {
                id = id
            });
        }

        public async Task EditAsync(TransactionDTO transaction)
        {
            await base.ExecuteStorageProcedureAsync("dbo.TransactionUpdate", new
            {
                id = transaction.Id,
                title = transaction.Title,
                description = transaction.Description,
                categoryId = transaction.CategoryId
            });
        }

        public async Task<TransactionDTO> GetAsync(int id)
        {
            IEnumerable<TransactionDTO> result = await base.LoadByStorageProcedureAsync<TransactionDTO>("dbo.TransactionGet", new
            {
                id = id
            });

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<TransactionDTO>> GetAllAsync()
        {
            return await base.LoadByStorageProcedureAsync<TransactionDTO>("dbo.TransactionGetAll", null);
        }
    }
}
