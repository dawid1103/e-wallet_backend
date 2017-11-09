using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EwalletCommon.Models;
using EwalletService.Repository;

namespace EwalletService.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<int> CreateAsync([FromBody] CategoryDTO category)
        {
            int id = await _categoryRepository.CreateAsync(category);
            return id;
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<CategoryDTO> GetAsync(int id)
        {
            return await _categoryRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        [HttpPut]
        public async Task UpdateAsync([FromBody] CategoryDTO category)
        {
            await _categoryRepository.EditAsync(category);
        }
    }
}
