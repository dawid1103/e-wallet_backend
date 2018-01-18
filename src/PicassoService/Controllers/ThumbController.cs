using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PicassoService.Services;
using System;
using System.IO;

namespace PicassoService.Controllers
{
    [Route("[controller]")]
    public class ThumbController : Controller
    {
        private readonly ILogger<ThumbController> logger;
        private readonly IImageProcessor imageProcessor;

        public ThumbController(ILogger<ThumbController> logger, IImageProcessor imageProcessor)
        {
            this.logger = logger;
            this.imageProcessor = imageProcessor;
        }

        [HttpGet("{fileName}")]
        public IActionResult GetFile([FromRoute]string fileName)
        {
            Stream stream = null;

            try
            {
                stream = imageProcessor.GetOriginal(fileName);
                return new FileStreamResult(stream, imageProcessor.RecognizeContentType(fileName)) { FileDownloadName = fileName };
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception happened by calling: [ThumbController - GetFile] with parameter fileName: {fileName}, EXCEPTION: {ex.ToString()}");
                return NotFound($"Image {fileName} not found");
            }
        }
    }
}