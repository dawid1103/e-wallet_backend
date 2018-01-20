using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PicassoService.Services;
using System.Threading.Tasks;

namespace PicassoService.Controllers
{
    [Route("[controller]")]
    public class UploadController : Controller
    {
        private readonly ILogger<UploadController> logger;
        private readonly IImageProcessor imageProcessor;

        public UploadController(ILogger<UploadController> logger, IImageProcessor imageProcessor)
        {
            this.logger = logger;
            this.imageProcessor = imageProcessor;
        }

        [HttpPost]
        public async Task<IActionResult> UpladFileAsync(IFormFile uploadFile)
        {
            if (uploadFile == null)
            {
                return BadRequest();
            }
            string fileName = await imageProcessor.SaveFile(uploadFile);
            return Created("path", fileName);
        }
    }
}