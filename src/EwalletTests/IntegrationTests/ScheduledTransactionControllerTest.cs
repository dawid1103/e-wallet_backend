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
    public class ScheduledTransactionControllerTest : TestBase
    {
        public ScheduledTransactionControllerTest() : base() { }

        [Fact]
        public async void CreateWithoutCategory()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int userId = await ewalletService.User.CreateAsync(user);

            ScheduledTransactionDTO transaction = TestData.GetScheduledTransactionData(userId);

            transaction.Id = await ewalletService.ScheduledTransaction.CreateAsync(transaction);

            Assert.NotNull(transaction.Id);
        }

        [Fact]
        public async void CreateWithCategory()
        {
            int userId = CreateUser();

            CategoryDTO category = TestData.GetCategoryData(userId);
            int categoryId = await ewalletService.Category.CreateAsync(category);

            ScheduledTransactionDTO transaction = TestData.GetScheduledTransactionData(userId, categoryId: categoryId);
            transaction.UserId = userId;

            transaction.Id = await ewalletService.ScheduledTransaction.CreateAsync(transaction);

            Assert.NotNull(transaction.Id);
        }

        [Fact]
        public async void CreateWithoutAssignedUser_ShouldThrowBadRequestException()
        {
            int userId = CreateUser();
            CategoryDTO category = TestData.GetCategoryData(userId);
            int categoryId = await ewalletService.Category.CreateAsync(category);

            ScheduledTransactionDTO transaction = TestData.GetScheduledTransactionData(0, categoryId);

            await Assert.ThrowsAsync<BadRequestException>(async () => await ewalletService.ScheduledTransaction.CreateAsync(transaction));
        }

        [Fact]
        public async void GetSingle()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int userId = await ewalletService.User.CreateAsync(user);

            ScheduledTransactionDTO transaction = TestData.GetScheduledTransactionData(userId);

            transaction.Id = await ewalletService.ScheduledTransaction.CreateAsync(transaction);

            Assert.NotNull(transaction.Id);

            ScheduledTransactionDTO fromDatabase = await ewalletService.ScheduledTransaction.GetAsync(transaction.Id);

            Assert.NotNull(fromDatabase);
            Assert.Equal(transaction.Id, fromDatabase.Id);
            Assert.Equal(transaction.Title, fromDatabase.Title);
            Assert.Equal(transaction.Description, fromDatabase.Description);
            Assert.Equal(transaction.Price, fromDatabase.Price);
            Assert.Equal(transaction.CategoryId, fromDatabase.CategoryId);
            Assert.Equal(transaction.AddDate.Date, fromDatabase.AddDate.Date);
            Assert.Equal(transaction.RepeatMode, fromDatabase.RepeatMode);
            Assert.Equal(transaction.RepeatDay, fromDatabase.RepeatDay);
            Assert.Equal(transaction.RepeatCount, fromDatabase.RepeatCount);
        }

        [Fact]
        public async void GetAll()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int userId = await ewalletService.User.CreateAsync(user);
            ScheduledTransactionDTO transaction = TestData.GetScheduledTransactionData(userId);

            await ewalletService.ScheduledTransaction.CreateAsync(transaction);
            await ewalletService.ScheduledTransaction.CreateAsync(transaction);

            IEnumerable<ScheduledTransactionDTO> transactions = await ewalletService.ScheduledTransaction.GetAllAsync();

            Assert.NotEmpty(transactions);
            Assert.True(transactions.Count() > 1);
        }

        [Fact]
        public async void Delete()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int userId = await ewalletService.User.CreateAsync(user);
            ScheduledTransactionDTO transaction = TestData.GetScheduledTransactionData(userId);
            int id = await ewalletService.ScheduledTransaction.CreateAsync(transaction);
            Assert.NotNull(id);

            await ewalletService.ScheduledTransaction.DeleteAsync(id);
            ScheduledTransactionDTO fromDatabase = await ewalletService.ScheduledTransaction.GetAsync(id);
            Assert.Null(fromDatabase);
        }

        [Fact]
        public async void Update()
        {
            int userId = CreateUser();
            CategoryDTO category = TestData.GetCategoryData(userId);
            int categoryId = await ewalletService.Category.CreateAsync(category);

            userId = CreateUser(); ;
            ScheduledTransactionDTO transaction = TestData.GetScheduledTransactionData(userId, categoryId: categoryId);
            transaction.Id = await ewalletService.ScheduledTransaction.CreateAsync(transaction);
            ScheduledTransactionDTO fromDatabase = await ewalletService.ScheduledTransaction.GetAsync(transaction.Id);

            string changedTitle = $"Test changed title {Guid.NewGuid().ToString()}";
            string changedDesc = $"Test changed description {Guid.NewGuid().ToString()}";
            decimal changedPrice = 231.23M;
            DateTime date = DateTime.Now.AddDays(4).Date;
            int repeatCount = 200;
            RepeatMode repeatMode = RepeatMode.Monthly;

            fromDatabase.Title = changedTitle;
            fromDatabase.Description = changedDesc;
            fromDatabase.Price = changedPrice;
            fromDatabase.CategoryId = 0;
            fromDatabase.RepeatCount = repeatCount;
            fromDatabase.RepeatMode = repeatMode;
            fromDatabase.RepeatDay = date;

            await ewalletService.ScheduledTransaction.UpdateAsync(fromDatabase);
            fromDatabase = await ewalletService.ScheduledTransaction.GetAsync(transaction.Id);

            Assert.Equal(transaction.Id, fromDatabase.Id);
            Assert.Equal(changedTitle, fromDatabase.Title);
            Assert.Equal(changedDesc, fromDatabase.Description);
            Assert.Equal(changedPrice, fromDatabase.Price);
            Assert.Equal(0, fromDatabase.CategoryId);
            Assert.Equal(repeatCount, fromDatabase.RepeatCount);
            Assert.Equal(repeatMode, fromDatabase.RepeatMode);
            Assert.Equal(date, fromDatabase.RepeatDay);
        }

        [Fact]
        public async void GetByUserId()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int userId = await ewalletService.User.CreateAsync(user);

            //create transaction 1
            ScheduledTransactionDTO transaction = TestData.GetScheduledTransactionData(userId);
            await ewalletService.ScheduledTransaction.CreateAsync(transaction);

            //create transaction 2
            transaction = TestData.GetScheduledTransactionData(userId);
            await ewalletService.ScheduledTransaction.CreateAsync(transaction);

            //check
            IEnumerable<ScheduledTransactionDTO> userTransactions = await ewalletService.ScheduledTransaction.GetAllByUserIdAsync(userId);
            Assert.True(userTransactions.Count() == 2);


            //create 2nd user
            user = TestData.GetUserRegistrationData();
            userId = await ewalletService.User.CreateAsync(user);

            //create transaction 1 for 2nd user
            transaction = TestData.GetScheduledTransactionData(userId);
            await ewalletService.ScheduledTransaction.CreateAsync(transaction);

            //check
            userTransactions = await ewalletService.ScheduledTransaction.GetAllByUserIdAsync(userId);
            Assert.True(userTransactions.Count() == 1);
        }
    }
}
