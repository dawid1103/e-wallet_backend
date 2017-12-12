using EwalletCommon.Models;
using EwalletTests.Common;
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
            int id = await _ewalletService.User.CreateAsync(user);

            ScheduledTransactionDTO transaction = TestData.GetScheduledTransactionData();
            transaction.UserId = id;

            transaction.Id = await _ewalletService.ScheduledTransaction.CreateAsync(transaction);

            Assert.NotNull(transaction.Id);
        }
    }
}
