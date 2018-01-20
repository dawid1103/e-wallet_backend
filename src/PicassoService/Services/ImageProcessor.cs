using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using PicassoService.Settings;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace PicassoService.Services
{
    public interface IImageProcessor
    {
        Stream GetOriginal(string fileName);
        string RecognizeContentType(string filePath);
        Task<string> SaveFile(IFormFile uploadFile);
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

        public async Task<string> SaveFile(IFormFile uploadFile)
        {
            Directory.CreateDirectory(Path.Combine(this.contentRootPath, settings.FileDirectory));

            string fileExtension = Path.GetExtension(uploadFile.FileName);
            string fileName = Path.ChangeExtension(Path.GetRandomFileName(), fileExtension);
            string path = Path.Combine(this.contentRootPath, settings.FileDirectory, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await uploadFile.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
