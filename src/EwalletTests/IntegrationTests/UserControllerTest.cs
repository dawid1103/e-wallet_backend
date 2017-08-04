using EwalletCommon.Endpoints;
using EwalletCommon.Models;
using EwalletTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EwalletTests.IntegrationTests
{
    [Collection(nameof(EwalletService))]

    public class UserControllerTest : TestBase
    {
        public UserControllerTest() : base() { }

        [Fact]
        public async void Create()
        {
            UserDTO user = TestData.GetUserData();
            int id = await _ewalletService.User.CreateAsync(user);
            Assert.NotNull(id);
        }

        [Fact]
        public async void GetSingle()
        {
            UserDTO user = TestData.GetUserData();
            user.Id = await _ewalletService.User.CreateAsync(user);

            Assert.NotNull(user.Id);

            UserDTO fromDatabase = await _ewalletService.User.GetAsync(user.Id);

            Assert.NotNull(fromDatabase);
            Assert.Equal(user.Id, fromDatabase.Id);
            Assert.Equal(user.Role, fromDatabase.Role);
            Assert.Equal(user.IsActive, fromDatabase.IsActive);
            Assert.NotNull(fromDatabase.PasswordHash);
            Assert.NotNull(fromDatabase.PasswordSalt);
            Assert.NotNull(fromDatabase.ModifiedDate);
            Assert.NotNull(fromDatabase.InsertedDate);
        }

        [Fact]
        public async void GetAll()
        {
            UserDTO user = TestData.GetUserData();
            await _ewalletService.User.CreateAsync(user);

            user = TestData.GetUserData();
            await _ewalletService.User.CreateAsync(user);

            IEnumerable<UserDTO> users = await _ewalletService.User.GetAllAsync();

            Assert.NotEmpty(users);
            Assert.True(users.Count() > 1);
        }

        [Fact]
        public async void Delete()
        {
            UserDTO user = TestData.GetUserData();
            int id = await _ewalletService.User.CreateAsync(user);
            Assert.NotNull(id);

            await _ewalletService.User.DeleteAsync(id);
        }

        [Fact]
        public async void GetByCredentials()
        {
            UserDTO user = TestData.GetUserData();
            user.Id = await _ewalletService.User.CreateAsync(user);

            Assert.NotNull(user.Id);

            UserVerificationResult verification = await _ewalletService.User.VerifyUser(user.Email, user.Password);

            Assert.True(verification.IsVerifiedAsPositive);
            Assert.Equal(user.Id, verification.Id);
            Assert.Equal(user.Role, verification.Role);
            Assert.Equal(user.Email, verification.Email);
        }
    }
}
