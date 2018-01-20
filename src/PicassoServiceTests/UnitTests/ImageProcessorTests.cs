using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using PicassoService.Services;
using PicassoService.Settings;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace PicassoServiceTests.UnitTests
{
    public class ImageProcessorTests
    {
        private static string FILE_DIRECTORY = "Images";
        private static string CONTENT_ROOT_PATH = "TestsFiles";
        private readonly Mock<IOptions<PicassoSettings>> optionsMock;
        private readonly Mock<IHostingEnvironment> hostingEnvironmentMock;

        public ImageProcessorTests()
        {
            optionsMock = new Mock<IOptions<PicassoSettings>>();
            optionsMock.Setup(m => m.Value).Returns(new PicassoSettings() { FileDirectory = FILE_DIRECTORY });

            hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            hostingEnvironmentMock.Setup(s => s.ContentRootPath).Returns(CONTENT_ROOT_PATH);
        }

        [Fact]
        public async Task SaveFile_ShouldUploadFile()
        {
            IImageProcessor imageProcessor = new ImageProcessor(optionsMock.Object, hostingEnvironmentMock.Object);
            IFormFile file = TestData.GetTestFile();
            string fileName = await imageProcessor.SaveFile(file);
            Assert.NotNull(fileName);
        }

        [Fact]
        public async Task GetFile_ShouldReturnFileStream()
        {
            string dirPath = Path.Combine(CONTENT_ROOT_PATH, FILE_DIRECTORY);
            Directory.CreateDirectory(dirPath);

            IFormFile file = TestData.GetTestFile();

            string fileExtension = Path.GetExtension(file.FileName);
            string fileName = Path.ChangeExtension(Path.GetRandomFileName(), fileExtension);
            string path = Path.Combine(dirPath, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            IImageProcessor imageProcessor = new ImageProcessor(optionsMock.Object, hostingEnvironmentMock.Object);
            FileStream fileStream = imageProcessor.GetOriginal(fileName) as FileStream;
            Assert.NotNull(fileStream);
        }
    }
}
