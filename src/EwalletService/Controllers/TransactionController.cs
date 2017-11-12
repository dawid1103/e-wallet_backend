using EwalletCommon.Models;
using EwalletService.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EwalletService.Controllers
{
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        private ITransactionRepository transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        [HttpPost]
        public async Task<int> CreateAsync([FromBody] TransactionDTO transaction)
        {
            int id = await transactionRepository.CreateAsync(transaction);
            return id;
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
        public async Task UpdateAsync([FromBody] TransactionDTO category)
        {
            await transactionRepository.EditAsync(category);
        }

        [HttpGet("user/{id}")]
        public async Task<IEnumerable<TransactionDTO>> GetAllByUserIdAsync(int id)
        {
            return await transactionRepository.GetAllByUserIdAsync(id);
        }
    }
}
