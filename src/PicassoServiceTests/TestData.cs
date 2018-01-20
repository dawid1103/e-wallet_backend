using Microsoft.AspNetCore.Http;
using Moq;
using System.IO;

namespace PicassoServiceTests
{
    public static class TestData
    {
        public static IFormFile GetTestFile()
        {
            string fileName = "test.txt";
            Stream ms = GetFileStream();

            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            return fileMock.Object;
        }

        public static Stream GetFileStream()
        {
            string content = "Hello World from a Fake File";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            return ms;
        }
    }
}
