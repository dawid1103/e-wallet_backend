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

            Assert.Equal(t1.Price + t2.Price, balance);
        }
    }
}
