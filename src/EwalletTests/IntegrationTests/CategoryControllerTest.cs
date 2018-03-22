using EwalletCommon.Models;
using EwalletTests.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Xunit;
using Xunit.Categories;

namespace EwalletTests.IntegrationTests
{
    [Collection(nameof(EwalletService))]
    [IntegrationTest]
    public class CategoryControllerTest : TestBase
    {
        [Fact]
        public async void Create_NoUser_ShouldThrow()
        {
            CategoryDTO category = TestData.GetCategoryData(0);
            await Assert.ThrowsAsync<SqlException>(async () => await ewalletService.Category.CreateAsync(category));
        }

        [Fact]
        public async void Create()
        {
            int userId = CreateUser();
            Assert.NotNull(userId);

            CategoryDTO category = TestData.GetCategoryData(userId);
            category.UserId = userId;

            int id = await ewalletService.Category.CreateAsync(category);
            Assert.NotNull(id);
        }

        [Fact]
        public async void GetSingle()
        {
            int userId = CreateUser();
            Assert.NotNull(userId);

            CategoryDTO category = TestData.GetCategoryData(userId);
            int id = await ewalletService.Category.CreateAsync(category);
            Assert.NotNull(id);

            CategoryDTO fromDatabase = await ewalletService.Category.GetAsync(id, category.UserId);

            Assert.NotNull(fromDatabase);
            Assert.Equal(category.Name, fromDatabase.Name);
        }

        [Fact]
        public async void Update()
        {
            int userId = CreateUser();
            Assert.NotNull(userId);

            CategoryDTO category = TestData.GetCategoryData(userId);
            category.Id = await ewalletService.Category.CreateAsync(category);
            CategoryDTO fromDatabase = await ewalletService.Category.GetAsync(category.Id, category.UserId);

            string changedName = $"Test changed {DateTime.Now.Ticks}";
            string changedColor = "rgba(255, 255, 255, 0.3)";
            fromDatabase.Name = changedName;
            fromDatabase.Color = changedColor;

            await ewalletService.Category.UpdateAsync(fromDatabase);
            fromDatabase = await ewalletService.Category.GetAsync(category.Id, category.UserId);

            Assert.Equal(changedName, fromDatabase.Name);
            Assert.Equal(changedColor, fromDatabase.Color);
        }

        [Fact]
        public async void Delete()
        {
            int userId = CreateUser();
            Assert.NotNull(userId);

            CategoryDTO category = TestData.GetCategoryData(userId);
            int id = await ewalletService.Category.CreateAsync(category);
            Assert.NotNull(id);

            await ewalletService.Category.DeleteAsync(id);

            CategoryDTO fromDatabase = await ewalletService.Category.GetAsync(id, category.UserId);
            Assert.Null(fromDatabase);
        }

        [Fact]
        public async void DeleteWithTransactions()
        {
            int userId = CreateUser();
            Assert.NotNull(userId);

            CategoryDTO category = TestData.GetCategoryData(userId);
            int categoryId = await ewalletService.Category.CreateAsync(category);

            TransactionDTO transaction = TestData.GetTransactionData(categoryId);
            transaction.Id = await ewalletService.Transaction.CreateAsync(transaction);

            Assert.NotNull(transaction.Id);

            await ewalletService.Category.DeleteAsync(categoryId);

            TransactionDTO transFromDb = await ewalletService.Transaction.GetAsync(transaction.Id);
            Assert.Equal(0, transFromDb.CategoryId);

            CategoryDTO catFromDb = await ewalletService.Category.GetAsync(transaction.CategoryId, category.UserId);
            Assert.Null(catFromDb);
        }

        [Fact]
        public async void GetAll()
        {
            int userId = CreateUser();
            Assert.NotNull(userId);

            //#1
            CategoryDTO category = TestData.GetCategoryData(userId);
            await ewalletService.Category.CreateAsync(category);

            //#2
            category = TestData.GetCategoryData(userId);
            await ewalletService.Category.CreateAsync(category);

            IEnumerable<CategoryDTO> categories = await ewalletService.Category.GetAllAsync(userId);

            Assert.NotEmpty(categories);
            Assert.True(categories.Count() > 1);
        }
    }
}
