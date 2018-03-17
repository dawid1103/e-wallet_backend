using EwalletCommon.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EwalletCommon.Endpoints
{
    public class CategoryEndpoint : ServiceEndpoint
    {
        public CategoryEndpoint(string url) : base(url)
        {
        }

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
        public async Task<CategoryDTO> GetAsync(int id, int userId)
        {
            return await base.GetAsync<CategoryDTO>($"category/{id}/user/{userId}");
        }

        /// <summary>
        /// Returns all categories
        /// </summary>
        /// <returns>IEnumerable<CategoryDTO></returns>
        public async Task<IEnumerable<CategoryDTO>> GetAllAsync(int userId)
        {
            return await base.GetAsync<IEnumerable<CategoryDTO>>($"category/user/{userId}");
        }


        /// <summary>
        /// Update given category
        /// </summary>
        /// <param name="category">CategoryDTO object</param>
        public async Task UpdateAsync(CategoryDTO category)
        {
            await base.PutAsync("category", category);
        }
    }
}
