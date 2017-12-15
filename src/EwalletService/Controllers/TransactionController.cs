﻿using EwalletCommon.Models;
using EwalletService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<TransactionController> logger;

        public TransactionController(ILogger<TransactionController> logger, ITransactionRepository transactionRepository)
        {
            this.logger = logger;
            this.transactionRepository = transactionRepository;
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

        [HttpPost("file")]
        public async Task<IActionResult> UpladFileAsync(IFormFile file)
        {
            string path = string.Empty;
            if (file != null)
            {
                path = Path.GetTempFileName().Replace("tmp", file.FileName.Split('.').Last());
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return Created("path", path);
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
