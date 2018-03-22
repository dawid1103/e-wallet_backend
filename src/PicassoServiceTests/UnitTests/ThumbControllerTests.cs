using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PicassoService.Controllers;
using PicassoService.Services;
using System.IO;
using Xunit;
using Xunit.Categories;

namespace PicassoServiceTests.UnitTests
{
    [UnitTest]
    public class ThumbControllerTests
    {
        [Fact]
        public void GetFileByName_ShouldReturnFileStrean()
        {
            string contentType = "text/plain";
            string fileName = "nameOfFile.txt";
            Stream ms = TestData.GetFileStream();

            var imageProcessorMock = new Mock<IImageProcessor>();
            imageProcessorMock.Setup(s => s.RecognizeContentType(It.IsAny<string>())).Returns(contentType);
            imageProcessorMock.Setup(s => s.GetOriginal(It.IsAny<string>())).Returns(ms);

            var loggerMock = new Mock<ILogger<ThumbController>>();
            var thumbController = new ThumbController(loggerMock.Object, imageProcessorMock.Object);
            var result = thumbController.GetFile(fileName) as FileStreamResult;

            Assert.NotNull(result);
            Assert.IsType<FileStreamResult>(result);
            Assert.NotNull(result.FileStream);
            Assert.Equal(ms.Length, result.FileStream.Length);
            Assert.Equal(contentType, result.ContentType);
            Assert.Equal(fileName, result.FileDownloadName);
        }

        [Fact]
        public void GetFileByName_NoFile_ShouldReturnNotFound()
        {
            var imageProcessorMock = new Mock<IImageProcessor>();
            imageProcessorMock.Setup(s => s.GetOriginal(It.IsAny<string>())).Throws(new DirectoryNotFoundException("Cannot find"));

            var loggerMock = new Mock<ILogger<ThumbController>>();
            var thumbController = new ThumbController(loggerMock.Object, imageProcessorMock.Object);

            string fileName = "nameOfFile.jpeg";
            var result = thumbController.GetFile(fileName) as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, result.StatusCode);

            string message = result.Value as string;
            Assert.NotNull(message);
        }
    }
}
