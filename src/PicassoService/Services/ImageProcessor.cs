using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using PicassoService.Settings;
using System.IO;

namespace PicassoService.Services
{
    public interface IImageProcessor
    {
        Stream GetOriginal(string fileName);
        string RecognizeContentType(string filePath);
    }

    public class ImageProcessor : IImageProcessor
    {
        public PicassoSettings settings;
        private readonly string contentRootPath;
        private readonly FileExtensionContentTypeProvider contentTypeProvider;

        public ImageProcessor(IOptions<PicassoSettings> settings, IHostingEnvironment env)
        {
            this.settings = settings.Value;
            contentRootPath = env.ContentRootPath;
            contentTypeProvider = new FileExtensionContentTypeProvider();
        }

        public Stream GetOriginal(string fileName)
        {
            var requestesFilePath = Path.Combine(contentRootPath, settings.FileDirectory, fileName);
            return new FileStream(requestesFilePath, FileMode.Open, FileAccess.Read);
        }

        public string RecognizeContentType(string filePath)
        {
            string contentType = "image/jpeg";
            contentTypeProvider.TryGetContentType(filePath, out contentType);
            return contentType;
        }
    }
}
