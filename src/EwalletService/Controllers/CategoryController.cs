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
        public async Task<IActionResult> CreateAsync([FromBody] CategoryDTO category)
        {
            int id = await categoryRepository.CreateAsync(category);
            return Created("id", id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await categoryRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/user/{userId}")]
        public async Task<CategoryDTO> GetAsync(int id, int userId)
        {
            return await categoryRepository.GetAsync(id, userId);
        }

        [HttpGet("user/{userId}")]
        public async Task<IEnumerable<CategoryDTO>> GetAllAsync(int userId)
        {
            return await categoryRepository.GetAllAsync(userId);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CategoryDTO category)
        {
            await categoryRepository.EditAsync(category);
            return NoContent();
        }
    }
}
