using EwalletCommon.Models;
using EwalletService.DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletService.Repository
{
    public interface ITransactionRepository : IRepository<TransactionDTO>
    {
        Task<IEnumerable<TransactionDTO>> GetAllByUserIdAsync(int id);
    }

    public class TransactionRepository : Repository, ITransactionRepository
    {
        public TransactionRepository(IDatabaseSession dbSession) : base(dbSession) { }

        public async Task<int> CreateAsync(TransactionDTO transaction)
        {
            IEnumerable<int> results = await base.LoadByStorageProcedureAsync<int>("dbo.TransactionCreate", new
            {
                title = transaction.Title,
                description = transaction.Description,
                type = transaction.Type,
                price = transaction.Price,
                filePath = transaction.FilePath,
                categoryId = transaction.CategoryId,
                userId = transaction.UserId
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
                type = transaction.Type,
                description = transaction.Description,
                price = transaction.Price,
                filePath = transaction.FilePath,
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

        public async Task<IEnumerable<TransactionDTO>> GetAllByUserIdAsync(int id)
        {
            IEnumerable<TransactionDTO> result = await base.LoadByStorageProcedureAsync<TransactionDTO>("dbo.TransactionGetAllByUserId", new
            {
                id = id
            });

            return result;
        }
    }
}
