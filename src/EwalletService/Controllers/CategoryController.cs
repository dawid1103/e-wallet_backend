using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EwalletCommon.Models;
using EwalletService.Repository;

namespace EwalletService.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<int> CreateAsync([FromBody] CategoryDTO category)
        {
            int id = await categoryRepository.CreateAsync(category);
            return id;
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await categoryRepository.DeleteAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<CategoryDTO> GetAsync(int id)
        {
            return await categoryRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            return await categoryRepository.GetAllAsync();
        }

        [HttpPut]
        public async Task UpdateAsync([FromBody] CategoryDTO category)
        {
            await categoryRepository.EditAsync(category);
        }
    }
}
