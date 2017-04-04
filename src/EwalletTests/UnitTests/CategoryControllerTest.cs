using EwalletCommon.Endpoints;
using EwalletCommon.Models;
using EwalletTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EwalletTests.UnitTests
{
    [Collection(nameof(EwalletService))]
    public class CategoryControllerTest : TestBase
    {
        public CategoryControllerTest() : base() { }

        private CategoryDTO GetCategoryData()
        {
            var category = new CategoryDTO()
            {
                Name = $"Category{DateTime.Now}"
            };

            return category;
        }

        [Fact]
        public async void Create()
        {
            CategoryDTO category = GetCategoryData();
            int id = await _ewalletService.Category.CreateAsync(category);
            Assert.NotNull(id);
        }

        [Fact]
        public async void GetSingle()
        {
            CategoryDTO category = GetCategoryData();
            int id = await _ewalletService.Category.CreateAsync(category);
            Assert.NotNull(id);

            CategoryDTO fromDatabase = await _ewalletService.Category.GetAsync(id);

            Assert.NotNull(fromDatabase);
            Assert.Equal(category.Name, fromDatabase.Name);
        }

        [Fact]
        public async void Update()
        {
            CategoryDTO category = GetCategoryData();
            category.Id = await _ewalletService.Category.CreateAsync(category);
            CategoryDTO fromDatabase = await _ewalletService.Category.GetAsync(category.Id);

            string changedNamee = $"Test changed {DateTime.Now.Ticks}";
            fromDatabase.Name = changedNamee;

            await _ewalletService.Category.UpdateAsync(fromDatabase);
            fromDatabase = await _ewalletService.Category.GetAsync(category.Id);

            Assert.Equal(fromDatabase.Name, changedNamee);
        }

        [Fact]
        public async void Delete()
        {
            CategoryDTO category = GetCategoryData();
            int id = await _ewalletService.Category.CreateAsync(category);
            Assert.NotNull(id);

            await _ewalletService.Category.DeleteAsync(id);

            CategoryDTO fromDatabase = await _ewalletService.Category.GetAsync(id);
            Assert.Null(fromDatabase);
        }

        [Fact]
        public async void GetAll()
        {
            CategoryDTO category = GetCategoryData();
            await _ewalletService.Category.CreateAsync(category);
            await _ewalletService.Category.CreateAsync(category);

            IEnumerable<CategoryDTO> categories = await _ewalletService.Category.GetAllAsync();

            Assert.NotEmpty(categories);
            Assert.True(categories.Count() > 1);
        }
    }
}
