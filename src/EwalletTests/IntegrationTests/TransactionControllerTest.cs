using EwalletCommon.Models;
using EwalletCommon.Utils;
using EwalletTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EwalletTests.IntegrationTests
{
    [Collection(nameof(EwalletService))]
    public class TransactionControllerTest : TestBase
    {
        public TransactionControllerTest() : base() { }

        [Fact]
        public async void CreateWithoutCategory()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int userId = await _ewalletService.User.CreateAsync(user);

            TransactionDTO transaction = TestData.GetTransactionData(userId);
            transaction.Id = await _ewalletService.Transaction.CreateAsync(transaction);

            Assert.NotNull(transaction.Id);
        }

        [Fact]
        public async void CreateWithoutAssignedUser_ShouldThrowBadRequestException()
        {
            CategoryDTO category = TestData.GetCategoryData();
            int categoryId = await _ewalletService.Category.CreateAsync(category);

            TransactionDTO transaction = TestData.GetTransactionData(0, categoryId);

            await Assert.ThrowsAsync<BadRequestException>(async () => await _ewalletService.Transaction.CreateAsync(transaction));
        }

        [Fact]
        public async void CreateWithCategory()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int userId = await _ewalletService.User.CreateAsync(user);

            CategoryDTO category = TestData.GetCategoryData();
            int categoryId = await _ewalletService.Category.CreateAsync(category);

            TransactionDTO transaction = TestData.GetTransactionData(userId, categoryId);

            transaction.Id = await _ewalletService.Transaction.CreateAsync(transaction);

            Assert.NotNull(transaction.Id);
            Assert.NotNull(transaction.CategoryId);
        }

        [Fact]
        public async void GetSingle()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int userId = await _ewalletService.User.CreateAsync(user);
            TransactionDTO transaction = TestData.GetTransactionData(userId);
            transaction.Id = await _ewalletService.Transaction.CreateAsync(transaction);

            Assert.NotNull(transaction.Id);

            TransactionDTO fromDatabase = await _ewalletService.Transaction.GetAsync(transaction.Id);

            Assert.NotNull(fromDatabase);
            Assert.Equal(transaction.Id, fromDatabase.Id);
            Assert.Equal(transaction.Title, fromDatabase.Title);
            Assert.Equal(transaction.Description, fromDatabase.Description);
            Assert.Equal(transaction.Price, fromDatabase.Price);
            Assert.Equal(transaction.CategoryId, fromDatabase.CategoryId);
            Assert.True(transaction.AddDate != fromDatabase.AddDate);
        }

        [Fact]
        public async void GetAll()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int userId = await _ewalletService.User.CreateAsync(user);
            TransactionDTO transaction = TestData.GetTransactionData(userId);

            await _ewalletService.Transaction.CreateAsync(transaction);
            await _ewalletService.Transaction.CreateAsync(transaction);

            IEnumerable<TransactionDTO> transactions = await _ewalletService.Transaction.GetAllAsync();

            Assert.NotEmpty(transactions);
            Assert.True(transactions.Count() > 1);
        }

        [Fact]
        public async void Delete()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int userId = await _ewalletService.User.CreateAsync(user);

            TransactionDTO transaction = TestData.GetTransactionData(userId);
            int id = await _ewalletService.Transaction.CreateAsync(transaction);
            Assert.NotNull(id);

            await _ewalletService.Transaction.DeleteAsync(id);
        }

        [Fact]
        public async void Update()
        {
            CategoryDTO category = TestData.GetCategoryData();
            int categoryId = await _ewalletService.Category.CreateAsync(category);

            TransactionDTO transaction = TestData.GetTransactionData(categoryId);
            transaction.Id = await _ewalletService.Transaction.CreateAsync(transaction);
            TransactionDTO fromDatabase = await _ewalletService.Transaction.GetAsync(transaction.Id);

            string changedTitle = $"Test changed title {DateTime.Now.Ticks}";
            string changedDesc = $"Test changed description {DateTime.Now.Ticks}";
            decimal changedPrice = 231.23M;

            fromDatabase.Title = changedTitle;
            fromDatabase.Description = changedDesc;
            fromDatabase.Price = changedPrice;
            fromDatabase.CategoryId = 0;

            await _ewalletService.Transaction.UpdateAsync(fromDatabase);
            fromDatabase = await _ewalletService.Transaction.GetAsync(transaction.Id);

            Assert.Equal(fromDatabase.Title, changedTitle);
            Assert.Equal(fromDatabase.Description, changedDesc);
            Assert.Equal(fromDatabase.Price, changedPrice);
            Assert.Equal(fromDatabase.CategoryId, 0);
        }

        [Fact]
        public async void GetByUserId()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int userId = await _ewalletService.User.CreateAsync(user);

            //create transaction 1
            TransactionDTO transaction = TestData.GetTransactionData(userId);
            await _ewalletService.Transaction.CreateAsync(transaction);

            //create transaction 2
            transaction = TestData.GetTransactionData(userId);
            await _ewalletService.Transaction.CreateAsync(transaction);

            //check
            IEnumerable<TransactionDTO> userTransactions = await _ewalletService.Transaction.GetAllByUserIdAsync(userId);
            Assert.True(userTransactions.Count() == 2);


            //create 2nd user
            user = TestData.GetUserRegistrationData();
            userId = await _ewalletService.User.CreateAsync(user);

            //create transaction 1 for 2nd user
            transaction = TestData.GetTransactionData(userId);
            await _ewalletService.Transaction.CreateAsync(transaction);

            //check
            userTransactions = await _ewalletService.Transaction.GetAllByUserIdAsync(userId);
            Assert.True(userTransactions.Count() == 1);
        }
    }
}
