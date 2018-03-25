using EwalletCommon.Enums;
using EwalletCommon.Models;
using EwalletService.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EwalletService.Controllers
{
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IAccountBalanceRepository accountBalanceRepository;
        private readonly ILogger<TransactionController> logger;

        public TransactionController(ILogger<TransactionController> logger, ITransactionRepository transactionRepository, IAccountBalanceRepository accountBalanceRepository)
        {
            this.logger = logger;
            this.transactionRepository = transactionRepository;
            this.accountBalanceRepository = accountBalanceRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TransactionDTO transaction)
        {
            if (transaction.UserId == 0)
            {
                logger.LogError("BadRequest - Model is not valid");
                return BadRequest("Model is not valid");
            }

            try
            {
                int id = await transactionRepository.CreateAsync(transaction);

                decimal amount = transaction.Price;
                if (transaction.Type == TransactionType.Expense)
                {
                    amount = transaction.Price * -1.00m;
                }

                await accountBalanceRepository.UpdateBalance(transaction.UserId, amount);
                return Created("id", id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, new object[] { });
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                TransactionDTO t = await transactionRepository.GetAsync(id);
                await transactionRepository.DeleteAsync(id);

                decimal amount = t.Price;
                if (t.Type == TransactionType.Income)
                {
                    amount = t.Price * -1.00m;
                }

                await accountBalanceRepository.UpdateBalance(t.UserId, amount);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, new object[] { });
                return new StatusCodeResult(500);
            }
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
        public async Task<IActionResult> UpdateAsync([FromBody] TransactionDTO transaction)
        {
            try
            {
                TransactionDTO t = await transactionRepository.GetAsync(transaction.Id);

                decimal amount = t.Price;
                if (t.Type == TransactionType.Income)
                {
                    amount = t.Price * -1.00m;
                }

                await accountBalanceRepository.UpdateBalance(t.UserId, amount);

                await transactionRepository.EditAsync(transaction);

                amount = transaction.Price;
                if (transaction.Type == TransactionType.Expense)
                {
                    amount = transaction.Price * -1.00m;
                }

                await accountBalanceRepository.UpdateBalance(t.UserId, amount);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, new object[] { });
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("user/{id}")]
        public async Task<IEnumerable<TransactionDTO>> GetAllByUserIdAsync(int id)
        {
            return await transactionRepository.GetAllByUserIdAsync(id);
        }
    }
}
