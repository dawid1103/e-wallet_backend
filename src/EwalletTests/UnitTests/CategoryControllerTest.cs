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
                Name = "Category" + DateTime.Now
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
        public async void Delete()
        {
            CategoryDTO category = GetCategoryData();
            int id = await _ewalletService.Category.CreateAsync(category);
            Assert.NotNull(id);

            await _ewalletService.Category.DeleteAsync(id);
        }
    }
}
