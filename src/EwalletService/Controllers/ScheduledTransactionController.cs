using EwalletCommon.Enums;
using EwalletCommon.Models;
using EwalletService.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EwalletService.Controllers
{
    [Route("[controller]")]
    public class ScheduledTransactionController : Controller
    {
        private readonly IScheduledTransactionRepository scheduledTransactionRepository;
        private readonly IAccountBalanceRepository accountBalanceRepository;

        public ScheduledTransactionController(IScheduledTransactionRepository scheduledTransactionRepository, 
            IAccountBalanceRepository accountBalanceRepository)
        {
            this.scheduledTransactionRepository = scheduledTransactionRepository;
            this.accountBalanceRepository = accountBalanceRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ScheduledTransactionDTO transaction)
        {
            if (transaction.UserId == 0)
            {
                return BadRequest();
            }

            int id = await scheduledTransactionRepository.CreateAsync(transaction);

            decimal amount = transaction.Price;
            if (transaction.Type == TransactionType.Expense)
            {
                amount = transaction.Price * -1.00m;
            }

            await accountBalanceRepository.UpdateBalance(transaction.UserId, amount);

            return Created("id", id);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
            => await scheduledTransactionRepository.DeleteAsync(id);

        [HttpGet("{id}")]
        public async Task<ScheduledTransactionDTO> GetAsync(int id)
            => await scheduledTransactionRepository.GetAsync(id);

        [HttpGet]
        public async Task<IEnumerable<ScheduledTransactionDTO>> GetAllAsync()
            => await scheduledTransactionRepository.GetAllAsync();

        [HttpPut]
        public async Task UpdateAsync([FromBody] ScheduledTransactionDTO transaction)
            => await scheduledTransactionRepository.EditAsync(transaction);

        [HttpGet("user/{id}")]
        public async Task<IEnumerable<ScheduledTransactionDTO>> GetAllByUserIdAsync(int id)
            => await scheduledTransactionRepository.GetAllByUserIdAsync(id);
    }
}