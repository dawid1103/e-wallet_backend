using EwalletCommon.Models;
using EwalletServices.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletServices.Repository
{
    public interface ICategoryRepository : IRepository<CategoryDTO>
    {
        Task GetAsync(string name);
    }
    public class CategoryRepository : Repository, ICategoryRepository
    {
        public CategoryRepository(IDatabaseSession dbSession) : base(dbSession) { }


        /// <summary>
        /// Add category to database async.
        /// </summary>
        /// <param name="category">CategoryDTO object</param>
        /// <returns>Created category id</returns>
        public async Task<int> AddAsync(CategoryDTO category)
        {
            IEnumerable<int> results = await base.LoadByStorageProcedureAsync<int>("dbo.CategoryCreate", new
            {
                Name = category.Name
            });

            return results.FirstOrDefault();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(CategoryDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDTO> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategoryDTO>> ListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
