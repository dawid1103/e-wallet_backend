using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PicassoService.Controllers;
using PicassoService.Services;
using System.Threading.Tasks;
using Xunit;

namespace PicassoServiceTests.UnitTests
{
    public class UploadControllerTests
    {

        [Fact]
        public void UploadFile_ShouldUploadFile_ReturnsFileName()
        {
            var loggerMock = new Mock<ILogger<UploadController>>();
            var imageProcessorMock = new Mock<IImageProcessor>();
            imageProcessorMock.Setup(s => s.SaveFile(It.IsAny<IFormFile>())).Returns(Task<string>.Factory.StartNew(() => "savedFileName.pdf"));

            UploadController controller = new UploadController(loggerMock.Object, imageProcessorMock.Object);
            IFormFile file = TestData.GetTestFile();
            IActionResult result = controller.UpladFileAsync(file).Result;

            var created = result as CreatedResult;
            Assert.NotNull(created);

            string fileName = created.Value as string;
            Assert.NotNull(fileName);
        }

        [Fact]
        public void UploadFile_NoFile_ReturnsBadRequest()
        {
            var loggerMock = new Mock<ILogger<UploadController>>();
            var imageProcessorMock = new Mock<IImageProcessor>();

            UploadController controller = new UploadController(loggerMock.Object, imageProcessorMock.Object);
            IFormFile file = null;
            IActionResult result = controller.UpladFileAsync(file).Result;

            Assert.NotNull(result);
            var badRequest = result as BadRequestResult;
            Assert.IsType<BadRequestResult>(badRequest);
            Assert.Equal(400, badRequest.StatusCode);
        }
    }
}
