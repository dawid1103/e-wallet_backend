using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PicassoService.Settings;
using System.IO;
using System.Threading.Tasks;

namespace PicassoService.Controllers
{
    [Route("[controller]")]
    public class UploadController : Controller
    {
        private readonly ILogger<UploadController> logger;
        private readonly string contentRootPath;
        public PicassoSettings settings;

        public UploadController(ILogger<UploadController> logger, IOptions<PicassoSettings> settings, IHostingEnvironment env)
        {
            this.logger = logger;
            this.contentRootPath = env.ContentRootPath;
            this.settings = settings.Value;
        }

        [HttpPost]
        public async Task<IActionResult> UpladFileAsync(IFormFile uploadFile)
        {
            Directory.CreateDirectory(Path.Combine(this.contentRootPath, settings.FileDirectory));

            string fileName = string.Empty;
            if (uploadFile != null)
            {
                string fileExtension = Path.GetExtension(uploadFile.FileName);
                fileName = Path.ChangeExtension(Path.GetRandomFileName(), fileExtension);

                string path = Path.Combine(this.contentRootPath, settings.FileDirectory, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await uploadFile.CopyToAsync(stream);
                }
            }

            return Created("path", fileName);
        }
    }
}