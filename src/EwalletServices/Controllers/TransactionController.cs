using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EwalletCommon.Models;
using EwalletServices.Repository;

namespace EwalletServices.Controllers
{
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        private ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpPost]
        public async Task<int> CreateAsync([FromBody] TransactionDTO transaction)
        {
            int id = await _transactionRepository.CreateAsync(transaction);
            return id;
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await _transactionRepository.DeleteAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<TransactionDTO> GetAsync(int id)
        {
            return await _transactionRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<TransactionDTO>> GetAllAsync()
        {
            return await _transactionRepository.GetAllAsync();
        }

        [HttpPut]
        public async Task UpdateAsync([FromBody] TransactionDTO category)
        {
            await _transactionRepository.EditAsync(category);
        }
    }
}
