using EwalletCommon.Models;
using EwalletTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EwalletTests.IntegrationTests
{
    [Collection(nameof(EwalletService))]
    public class CategoryControllerTest : TestBase
    {
        [Fact]
        public async void Create()
        {
            CategoryDTO category = TestData.GetCategoryData();
            int id = await ewalletService.Category.CreateAsync(category);
            Assert.NotNull(id);
        }

        [Fact]
        public async void GetSingle()
        {
            CategoryDTO category = TestData.GetCategoryData();
            int id = await ewalletService.Category.CreateAsync(category);
            Assert.NotNull(id);

            CategoryDTO fromDatabase = await ewalletService.Category.GetAsync(id);

            Assert.NotNull(fromDatabase);
            Assert.Equal(category.Name, fromDatabase.Name);
        }

        [Fact]
        public async void Update()
        {
            CategoryDTO category = TestData.GetCategoryData();
            category.Id = await ewalletService.Category.CreateAsync(category);
            CategoryDTO fromDatabase = await ewalletService.Category.GetAsync(category.Id);

            string changedName = $"Test changed {DateTime.Now.Ticks}";
            string changedColor = "rgba(255, 255, 255, 0.3)";
            fromDatabase.Name = changedName;
            fromDatabase.Color = changedColor;

            await ewalletService.Category.UpdateAsync(fromDatabase);
            fromDatabase = await ewalletService.Category.GetAsync(category.Id);

            Assert.Equal(changedName, fromDatabase.Name);
            Assert.Equal(changedColor, fromDatabase.Color);
        }

        [Fact]
        public async void Delete()
        {
            CategoryDTO category = TestData.GetCategoryData();
            int id = await ewalletService.Category.CreateAsync(category);
            Assert.NotNull(id);

            await ewalletService.Category.DeleteAsync(id);

            CategoryDTO fromDatabase = await ewalletService.Category.GetAsync(id);
            Assert.Null(fromDatabase);
        }

        [Fact]
        public async void DeleteWithTransactions()
        {
            CategoryDTO category = TestData.GetCategoryData();
            int categoryId = await ewalletService.Category.CreateAsync(category);

            TransactionDTO transaction = TestData.GetTransactionData(categoryId);
            transaction.Id = await ewalletService.Transaction.CreateAsync(transaction);

            Assert.NotNull(transaction.Id);

            await ewalletService.Category.DeleteAsync(categoryId);

            TransactionDTO transFromDb = await ewalletService.Transaction.GetAsync(transaction.Id);
            Assert.Equal(0, transFromDb.CategoryId);

            CategoryDTO catFromDb = await ewalletService.Category.GetAsync(transaction.CategoryId);
            Assert.Null(catFromDb);

        }

        [Fact]
        public async void GetAll()
        {
            CategoryDTO category = TestData.GetCategoryData();
            await ewalletService.Category.CreateAsync(category);
            await ewalletService.Category.CreateAsync(category);

            IEnumerable<CategoryDTO> categories = await ewalletService.Category.GetAllAsync();

            Assert.NotEmpty(categories);
            Assert.True(categories.Count() > 1);
        }
    }
}
