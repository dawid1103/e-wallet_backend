using EwalletCommon.Models;
using EwalletService.Logic;
using EwalletService.Models;
using EwalletService.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletService.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IPasswordLogic passwordLogic;
        private readonly IUserRepository userRepository;

        public UserController(IPasswordLogic passwordLogic, IUserRepository userRepository)
        {
            this.passwordLogic = passwordLogic;
            this.userRepository = userRepository;
        }

        [HttpPost]
        public async Task<int> CreateAsync([FromBody]UserRegistrationDataDTO userData)
        {
            UserCredentials credentials = passwordLogic.HashPassword(userData.Password);
            UserDTO user = new UserDTO(userData.Email, credentials.Salt, credentials.Hash);
            int userId = await userRepository.CreateAsync(user);
            return userId;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await userRepository.DeleteAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<UserDTO> GetAsync(int id)
        {
            return await userRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            return await userRepository.GetAllAsync();
        }

        [HttpPost("verifyuser")]
        public async Task<UserVerificationResultDTO> VerifyUser([FromQuery]string email, [FromBody]string password)
        {
            IEnumerable<UserDTO> allUsers = await userRepository.GetAllAsync();
            UserDTO user = allUsers.First(u => u.Email == email);

            string calculatedHash = passwordLogic.HashPassword(password, user.Salt);
            bool isMatching = user.PasswordHash == calculatedHash;

            return new UserVerificationResultDTO
            {
                Id = user.Id,
                IsVerifiedAsPositive = isMatching && user.IsActive,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
