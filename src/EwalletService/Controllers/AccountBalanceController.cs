using EwalletService.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EwalletService.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    public class AccountBalanceController : Controller
    {
        private readonly ILogger<AccountBalanceController> logger;
        private readonly IAccountBalanceRepository accountBalanceRepository;

        public AccountBalanceController(ILogger<AccountBalanceController> logger, 
            IAccountBalanceRepository accountBalanceRepository)
        {
            this.logger = logger;
            this.accountBalanceRepository = accountBalanceRepository;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            decimal amount = await accountBalanceRepository.GetBalace(userId);
            return Ok(amount);
        }
    }
}
