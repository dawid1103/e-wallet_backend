using EwalletCommon.Models;
using EwalletService.Controllers;
using EwalletService.Repository;
using EwalletTests.Common;
using Moq;
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
            //Arrange 
            Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<CategoryDTO>()))
                                  .ReturnsAsync(2);

            CategoryController categoryController = new CategoryController(categoryRepositoryMock.Object);

            CategoryDTO category = TestData.GetCategoryData();
            int id = await categoryController.CreateAsync(category);

            Assert.NotNull(id);
            Assert.Equal(2, id);
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
            Assert.Equal(0, transFromDb.CategoryId);

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
