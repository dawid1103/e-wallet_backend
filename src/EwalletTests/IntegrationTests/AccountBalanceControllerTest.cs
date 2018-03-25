using EwalletCommon.Enums;
using EwalletCommon.Models;
using EwalletTests.Common;
using Xunit;
using Xunit.Categories;

namespace EwalletTests.IntegrationTests
{
    [Collection(nameof(EwalletService))]
    [IntegrationTest]
    public class AccountBalanceControllerTest : TestBase
    {
        [Fact]
        public async void CreateTransaction_Should_AddAmountToAccountBalance()
        {
            decimal expected = 0.00m;

            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int userId = await ewalletService.User.CreateAsync(user);

            TransactionDTO t1 = TestData.GetTransactionData(userId);
            t1.Type = TransactionType.Income;
            await ewalletService.Transaction.CreateAsync(t1);

            TransactionDTO t2 = TestData.GetTransactionData(userId);
            t2.Type = TransactionType.Income;
            await ewalletService.Transaction.CreateAsync(t2);

            decimal balance = await ewalletService.AccountBalance.GetAsync(userId);

            expected = t1.Price + t2.Price;
            Assert.Equal(expected, balance);
        }

        [Fact]
        public async void DeleteTransaction_Should_TakeIntoAccountChangesInTheBalanceOfTheAccount()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int userId = await ewalletService.User.CreateAsync(user);

            TransactionDTO t1 = TestData.GetTransactionData(userId);
            t1.Type = TransactionType.Expense;
            int t1Id = await ewalletService.Transaction.CreateAsync(t1);

            TransactionDTO t2 = TestData.GetTransactionData(userId);
            t2.Type = TransactionType.Income;
            int t2Id = await ewalletService.Transaction.CreateAsync(t2);

            await ewalletService.Transaction.DeleteAsync(t2Id);

            decimal balance = await ewalletService.AccountBalance.GetAsync(userId);

            decimal expected = t1.Price * -1;
            Assert.Equal(expected, balance);
        }

        [Fact]
        public async void UpdateTransaction_Should_TakeIntoAccountChangesInTheBalanceOfTheAccount()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int userId = await ewalletService.User.CreateAsync(user);

            TransactionDTO t1 = TestData.GetTransactionData(userId);
            t1.Type = TransactionType.Expense;
            int t1Id = await ewalletService.Transaction.CreateAsync(t1);

            TransactionDTO t2 = TestData.GetTransactionData(userId);
            t2.Type = TransactionType.Income;
            t2.Id = await ewalletService.Transaction.CreateAsync(t2);

            t2.Type = TransactionType.Expense;
            t2.Price = 123.98m;

            await ewalletService.Transaction.UpdateAsync(t2);

            decimal balance = await ewalletService.AccountBalance.GetAsync(userId);

            decimal expected = (t1.Price * -1) + (t2.Price * -1);
            Assert.Equal(expected, balance);
        }
    }
}
