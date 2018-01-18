using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Moq;
using PicassoService.Settings;

namespace PicassoServiceTests.UnitTests
{
    public abstract class UnitTestBase
    {
        protected readonly Mock<IOptions<PicassoSettings>> optionsMock;
        protected readonly Mock<IHostingEnvironment> hostingEnvironmentMock;

        public UnitTestBase()
        {
            optionsMock = new Mock<IOptions<PicassoSettings>>();
            optionsMock.Setup(m => m.Value).Returns(new PicassoSettings() { FileDirectory = "Images" });

            hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            hostingEnvironmentMock.Setup(s => s.ContentRootPath).Returns("TestsFiles");
        }
    }
}
