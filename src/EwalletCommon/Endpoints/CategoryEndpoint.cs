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

        public async Task<int> CreateAsync(CategoryDTO category)
        {
            return await PostAsync<int>("category", category);
        }

        public async Task DeleteAsync(int id)
        {
            await DeleteAsync($"category/{id}");
        }
    }
}
