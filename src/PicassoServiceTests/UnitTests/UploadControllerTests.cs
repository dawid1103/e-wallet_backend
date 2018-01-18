using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PicassoService.Controllers;
using Xunit;

namespace PicassoServiceTests.UnitTests
{
    public class UploadControllerTests : UnitTestBase
    {

        [Fact]
        public void UploadFile_ShouldUploadFile_ReturnsFileName()
        {
            var loggerMock = new Mock<ILogger<UploadController>>();
            UploadController controller = new UploadController(loggerMock.Object, optionsMock.Object, hostingEnvironmentMock.Object);
            IFormFile file = TestData.GetTestFile();

            IActionResult result = controller.UpladFileAsync(file).Result;

            var created = result as CreatedResult;
            Assert.NotNull(created);

            string fileName = created.Value as string;
            Assert.NotNull(fileName);
        }
    }
}
