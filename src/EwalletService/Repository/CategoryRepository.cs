using EwalletCommon.Models;
using EwalletService.DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletService.Repository
{
    public interface ICategoryRepository
    {
        Task<CategoryDTO> GetAsync(int id, int userId);

        Task<IEnumerable<CategoryDTO>> GetAllAsync(int userId);

        Task<int> CreateAsync(CategoryDTO category);

        Task DeleteAsync(int id);

        Task EditAsync(CategoryDTO category);
    }

    public class CategoryRepository : Repository, ICategoryRepository
    {
        public CategoryRepository(IDatabaseSession dbSession) : base(dbSession) { }

        public async Task<int> CreateAsync(CategoryDTO category)
        {
            IEnumerable<int> results = await base.LoadByStorageProcedureAsync<int>("dbo.CategoryCreate", new
            {
                Name = category.Name,
                Color = category.Color,
                UserId = category.UserId
            });

            return results.FirstOrDefault();
        }

        public async Task DeleteAsync(int id)
        {
            await base.ExecuteStorageProcedureAsync("dbo.CategoryDelete", new
            {
                id = id
            });
        }

        public async Task EditAsync(CategoryDTO category)
        {
            await base.ExecuteStorageProcedureAsync("dbo.CategoryUpdate", new
            {
                id = category.Id,
                name = category.Name,
                color = category.Color
            });
        }

        public async Task<CategoryDTO> GetAsync(int id, int userId)
        {
            IEnumerable<CategoryDTO> result = await base.LoadByStorageProcedureAsync<CategoryDTO>("dbo.CategoryGet", new
            {
                id = id,
                userId = userId
            });

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync(int userId)
        {
            return await base.LoadByStorageProcedureAsync<CategoryDTO>("dbo.CategoryGetAll", new
            {
                userId = userId
            });
        }
    }
}
