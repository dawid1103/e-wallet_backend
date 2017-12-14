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
        private IScheduledTransactionRepository scheduledTransactionRepository;

        public ScheduledTransactionController(IScheduledTransactionRepository scheduledTransactionRepository)
        {
            this.scheduledTransactionRepository = scheduledTransactionRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ScheduledTransactionDTO transaction)
        {
            if (transaction.UserId == 0)
            {
                return BadRequest();
            }

            int id = await scheduledTransactionRepository.CreateAsync(transaction);

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