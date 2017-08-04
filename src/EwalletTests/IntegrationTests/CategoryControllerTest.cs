using EwalletCommon.Endpoints;
using EwalletCommon.Models;
using EwalletTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EwalletTests.IntegrationTests
{
    [Collection(nameof(EwalletService))]
    public class CategoryControllerTest : TestBase
    {

        public CategoryControllerTest() : base() { }

        [Fact]
        public async void Create()
        {
            CategoryDTO category = TestData.GetCategoryData();
            int id = await _ewalletService.Category.CreateAsync(category);
            Assert.NotNull(id);
        }

        [Fact]
        public async void GetSingle()
        {
            CategoryDTO category = TestData.GetCategoryData();
            int id = await _ewalletService.Category.CreateAsync(category);
            Assert.NotNull(id);

            CategoryDTO fromDatabase = await _ewalletService.Category.GetAsync(id);

            Assert.NotNull(fromDatabase);
            Assert.Equal(category.Name, fromDatabase.Name);
        }

        [Fact]
        public async void Update()
        {
            CategoryDTO category = TestData.GetCategoryData();
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
            CategoryDTO category = TestData.GetCategoryData();
            int id = await _ewalletService.Category.CreateAsync(category);
            Assert.NotNull(id);

            await _ewalletService.Category.DeleteAsync(id);

            CategoryDTO fromDatabase = await _ewalletService.Category.GetAsync(id);
            Assert.Null(fromDatabase);
        }

        [Fact]
        public async void DeleteWithTransactions()
        {
            CategoryDTO category = TestData.GetCategoryData();
            int categoryId = await _ewalletService.Category.CreateAsync(category);

            TransactionDTO transaction = TestData.GetTransactionData(categoryId);
            transaction.Id = await _ewalletService.Transaction.CreateAsync(transaction);

            Assert.NotNull(transaction.Id);

            await _ewalletService.Category.DeleteAsync(transaction.CategoryId);

            TransactionDTO transFromDb = await _ewalletService.Transaction.GetAsync(transaction.Id);
            Assert.Equal(transFromDb.CategoryId, 0);

            CategoryDTO catFromDb = await _ewalletService.Category.GetAsync(transaction.CategoryId);
            Assert.Null(catFromDb);

        }

        [Fact]
        public async void GetAll()
        {
            CategoryDTO category = TestData.GetCategoryData();
            await _ewalletService.Category.CreateAsync(category);
            await _ewalletService.Category.CreateAsync(category);

            IEnumerable<CategoryDTO> categories = await _ewalletService.Category.GetAllAsync();

            Assert.NotEmpty(categories);
            Assert.True(categories.Count() > 1);
        }
    }
}
