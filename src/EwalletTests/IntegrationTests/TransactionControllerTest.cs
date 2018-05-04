using EwalletCommon.Enums;
using EwalletCommon.Models;
using EwalletCommon.Utils;
using EwalletTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Categories;

namespace EwalletTests.IntegrationTests
{
    [Collection(nameof(EwalletService))]
    [IntegrationTest]
    public class TransactionControllerTest : TestBase
    {
        public TransactionControllerTest() : base() { }

        [Fact]
        public async void CreateWithoutCategory()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int userId = await ewalletService.User.CreateAsync(user);

            TransactionDTO transaction = TestData.GetTransactionData(userId);
            transaction.Id = await ewalletService.Transaction.CreateAsync(transaction);

            Assert.NotNull(transaction.Id);
        }

        [Fact]
        public async void CreateWithoutAssignedUser_ShouldThrowBadRequestException()
        {
            int userId = CreateUser();
            CategoryDTO category = TestData.GetCategoryData(userId);
            int categoryId = await ewalletService.Category.CreateAsync(category);

            TransactionDTO transaction = TestData.GetTransactionData(0, categoryId);

            await Assert.ThrowsAsync<BadRequestException>(async () => await ewalletService.Transaction.CreateAsync(transaction));
        }

        [Fact]
        public async void CreateWithCategory()
        {
            int userId = CreateUser();
            CategoryDTO category = TestData.GetCategoryData(userId);
            int categoryId = await ewalletService.Category.CreateAsync(category);

            TransactionDTO transaction = TestData.GetTransactionData(userId, categoryId);

            transaction.Id = await ewalletService.Transaction.CreateAsync(transaction);

            Assert.NotNull(transaction.Id);
            Assert.NotNull(transaction.CategoryId);
        }

        [Fact]
        public async void GetSingle()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int userId = await ewalletService.User.CreateAsync(user);
            TransactionDTO transaction = TestData.GetTransactionData(userId);
            transaction.Id = await ewalletService.Transaction.CreateAsync(transaction);

            Assert.NotNull(transaction.Id);

            TransactionDTO fromDatabase = await ewalletService.Transaction.GetAsync(transaction.Id);

            Assert.NotNull(fromDatabase);
            Assert.Equal(transaction.Id, fromDatabase.Id);
            Assert.Equal(transaction.Title, fromDatabase.Title);
            Assert.Equal(transaction.Description, fromDatabase.Description);
            Assert.Equal(transaction.Price, fromDatabase.Price);
            Assert.Equal(transaction.CategoryId, fromDatabase.CategoryId);
            Assert.True(transaction.AddDate != fromDatabase.AddDate);
            Assert.Equal(transaction.Type, fromDatabase.Type);
        }

        [Fact]
        public async void Delete()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int userId = await ewalletService.User.CreateAsync(user);

            TransactionDTO transaction = TestData.GetTransactionData(userId);
            int id = await ewalletService.Transaction.CreateAsync(transaction);
            Assert.NotNull(id);

            await ewalletService.Transaction.DeleteAsync(id);
        }

        [Fact]
        public async void Update()
        {
            int userId = CreateUser();
            CategoryDTO category = TestData.GetCategoryData(userId);
            int categoryId = await ewalletService.Category.CreateAsync(category);

            TransactionDTO transaction = TestData.GetTransactionData(categoryId);
            transaction.Id = await ewalletService.Transaction.CreateAsync(transaction);
            TransactionDTO fromDatabase = await ewalletService.Transaction.GetAsync(transaction.Id);

            string changedTitle = $"Test changed title {DateTime.Now.Ticks}";
            string changedDesc = $"Test changed description {DateTime.Now.Ticks}";
            decimal changedPrice = 231.23M;

            fromDatabase.Title = changedTitle;
            fromDatabase.Description = changedDesc;
            fromDatabase.Price = changedPrice;
            fromDatabase.CategoryId = 0;
            fromDatabase.Type = TransactionType.Expense;

            await ewalletService.Transaction.UpdateAsync(fromDatabase);
            fromDatabase = await ewalletService.Transaction.GetAsync(transaction.Id);

            Assert.Equal(changedTitle, fromDatabase.Title);
            Assert.Equal(changedDesc, fromDatabase.Description);
            Assert.Equal(changedPrice, fromDatabase.Price);
            Assert.Equal(TransactionType.Expense, fromDatabase.Type);
            Assert.Equal(0, fromDatabase.CategoryId);
        }

        [Fact]
        public async void GetByUserId()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int userId = await ewalletService.User.CreateAsync(user);

            //create transaction 1
            TransactionDTO transaction = TestData.GetTransactionData(userId);
            await ewalletService.Transaction.CreateAsync(transaction);

            //create transaction 2
            transaction = TestData.GetTransactionData(userId);
            await ewalletService.Transaction.CreateAsync(transaction);

            //check
            IEnumerable<TransactionDTO> userTransactions = await ewalletService.Transaction.GetAllAsync(userId);
            Assert.True(userTransactions.Count() == 2);


            //create 2nd user
            user = TestData.GetUserRegistrationData();
            userId = await ewalletService.User.CreateAsync(user);

            //create transaction 1 for 2nd user
            transaction = TestData.GetTransactionData(userId);
            await ewalletService.Transaction.CreateAsync(transaction);

            //check
            userTransactions = await ewalletService.Transaction.GetAllAsync(userId);
            Assert.True(userTransactions.Count() == 1);
        }
    }
}
