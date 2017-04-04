using EwalletCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EwalletCommon.Endpoints
{
    public class CategoryEndpoint : ServiceEndpoint
    {
        public CategoryEndpoint(HttpClient client) : base(client)
        {
        }

        /// <summary>
        /// Create category
        /// </summary>
        /// <param name="category">CategoryDTO object</param>
        /// <returns>Category id of created</returns>
        public async Task<int> CreateAsync(CategoryDTO category)
        {
            return await PostAsync<int>("category", category);
        }

        /// <summary>
        /// Delete category of given category id
        /// </summary>
        /// <param name="id">category id</param>
        public async Task DeleteAsync(int id)
        {
            await DeleteAsync($"category/{id}");
        }

        /// <summary>
        /// Returns CategoryDTO object of category with gicen category id
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>CategoryDTO object</returns>
        public async Task<CategoryDTO> GetAsync(int id)
        {
            return await base.GetAsync<CategoryDTO>($"category/{id}");
        }

        /// <summary>
        /// Returns all categories
        /// </summary>
        /// <returns>IEnumerable<CategoryDTO></returns>
        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            return await base.GetAsync<IEnumerable<CategoryDTO>>("category");
        }


        /// <summary>
        /// Update given categoyr
        /// </summary>
        /// <param name="category">CategoryDTO object</param>
        public async Task UpdateAsync(CategoryDTO category)
        {
            await base.PutAsync("category", category);
        }
    }
}
