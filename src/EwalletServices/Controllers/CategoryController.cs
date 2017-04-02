using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EwalletCommon.Models;
using EwalletServices.Repository;

namespace EwalletServices.Controllers
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
        public async Task<int> Create([FromBody] CategoryDTO category)
        {
            int id = await _categoryRepository.AddAsync(category);
            return id;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<CategoryDTO> GetById(int id)
        {
            return await _categoryRepository.GetAsync(id);
        }
    }
}
