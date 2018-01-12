using EwalletCommon.Models;
using EwalletService.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletService.Controllers
{
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IHostingEnvironment env;
        private readonly FileExtensionContentTypeProvider contentTypeProvider;
        private readonly ILogger<TransactionController> logger;

        public TransactionController(ILogger<TransactionController> logger, ITransactionRepository transactionRepository, IHostingEnvironment env)
        {
            this.logger = logger;
            this.transactionRepository = transactionRepository;
            this.env = env;
            contentTypeProvider = new FileExtensionContentTypeProvider();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TransactionDTO transaction)
        {
            if (transaction.UserId == 0)
            {
                logger.LogError("BadRequest - Model is not valid");
                return BadRequest("Model is not valid");
            }

            int id = await transactionRepository.CreateAsync(transaction);
            return Created("id", id);
        }


        //TODO: Zrobić osobna usługe do przetwarzania obrazków
        [HttpPost("file")]
        public async Task<IActionResult> UpladFileAsync(IFormFile uploadFile)
        {
            string fileDirectory = "TransactionImages";
            Directory.CreateDirectory(Path.Combine(env.ContentRootPath, fileDirectory));

            string fileName = string.Empty;
            if (uploadFile != null)
            {
                string fileExtension = Path.GetExtension(uploadFile.FileName);
                fileName = Path.ChangeExtension(Path.GetRandomFileName(), fileExtension);

                string path = Path.Combine(fileDirectory, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await uploadFile.CopyToAsync(stream);
                }
            }

            return Created("path", fileName);
        }

        //TODO: Zrobić osobna usługe do przetwarzania obrazków
        [HttpGet("file/{fileName}")]
        public IActionResult GetFile([FromRoute]string fileName)
        {
            Stream stream = null;

            try
            {
                stream = GetOriginal(fileName);
                return new FileStreamResult(stream, RecognizeContentType(fileName));
            }

            catch (Exception exc)
            {
                logger.LogError($"Exception happened by calling: [{Request.Path}], EXCEPTION: {exc.ToString()}");
                return BadRequest();
            }
        }

        //TODO: Zrobić osobna usługe do przetwarzania obrazków
        public Stream GetOriginal(string fileName)
        {
            var requestesFilePath = Path.Combine("TransactionImages", fileName);
            return new FileStream(requestesFilePath, FileMode.Open, FileAccess.Read);
        }

        //TODO: Zrobić osobna usługe do przetwarzania obrazków
        private string RecognizeContentType(string filePath)
        {
            string contentType = "image/jpeg";
            contentTypeProvider.TryGetContentType(filePath, out contentType);
            return contentType;
        }


        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await transactionRepository.DeleteAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<TransactionDTO> GetAsync(int id)
        {
            return await transactionRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<TransactionDTO>> GetAllAsync()
        {
            return await transactionRepository.GetAllAsync();
        }

        [HttpPut]
        public async Task UpdateAsync([FromBody] TransactionDTO transaction)
        {
            await transactionRepository.EditAsync(transaction);
        }

        [HttpGet("user/{id}")]
        public async Task<IEnumerable<TransactionDTO>> GetAllByUserIdAsync(int id)
        {
            return await transactionRepository.GetAllByUserIdAsync(id);
        }
    }
}
