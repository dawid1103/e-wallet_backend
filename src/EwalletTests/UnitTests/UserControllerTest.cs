using EwalletCommon.Endpoints;
using EwalletCommon.Models;
using EwalletTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EwalletTests.UnitTests
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
    }
}
