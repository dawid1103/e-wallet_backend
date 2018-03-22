using EwalletCommon.Enums;
using EwalletCommon.Models;
using EwalletTests.Common;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Categories;

namespace EwalletTests.IntegrationTests
{
    [Collection(nameof(EwalletService))]

    [IntegrationTest]
    public class UserControllerTest : TestBase
    {
        public UserControllerTest() : base() { }

        [Fact]
        public async void Create()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int id = await ewalletService.User.CreateAsync(user);
            Assert.NotNull(id);
        }

        [Fact]
        public async void GetSingle()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int id = await ewalletService.User.CreateAsync(user);

            Assert.NotNull(id);

            UserDTO fromDatabase = await ewalletService.User.GetAsync(id);

            Assert.NotNull(fromDatabase);
            Assert.Equal(id, fromDatabase.Id);
            Assert.Equal(UserRole.Admin, fromDatabase.Role);
            Assert.Equal(true, fromDatabase.IsActive);
            Assert.NotNull(fromDatabase.PasswordHash);
            Assert.NotNull(fromDatabase.Salt);
            Assert.NotNull(fromDatabase.ModifiedDate);
            Assert.NotNull(fromDatabase.InsertedDate);
        }

        [Fact]
        public async void GetAll()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            await ewalletService.User.CreateAsync(user);

            user = TestData.GetUserRegistrationData();
            await ewalletService.User.CreateAsync(user);

            IEnumerable<UserDTO> users = await ewalletService.User.GetAllAsync();

            Assert.NotEmpty(users);
            Assert.True(users.Count() > 1);
        }

        [Fact]
        public async void Delete()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int id = await ewalletService.User.CreateAsync(user);
            Assert.NotNull(id);

            await ewalletService.User.DeleteAsync(id);
        }

        [Fact]
        public async void GetByCredentials()
        {
            UserRegistrationDataDTO user = TestData.GetUserRegistrationData();
            int id = await ewalletService.User.CreateAsync(user);

            Assert.NotNull(id);

            UserVerificationResultDTO verification = await ewalletService.User.VerifyUser(user.Email, user.Password);

            Assert.True(verification.IsVerifiedAsPositive);
            Assert.Equal(id, verification.Id);
            Assert.Equal(UserRole.Admin, verification.Role);
            Assert.Equal(user.Email, verification.Email);
        }
    }
}
