using EwalletCommon.Enums;
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
        public async Task<int> CreateAsync([FromBody]UserDTO user)
        {
            UserCredentials credentials = passwordLogic.HashPassword(user.Password);
            user.PasswordHash = credentials.Hash;
            user.PasswordSalt = credentials.Salt;

            //temporary
            user.Role = UserRole.Admin;

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

        //TODO later
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]UserDTO user)
        //{
        //}

        [HttpPost("verifyuser")]
        public async Task<UserVerificationResult> VerifyUser([FromQuery]string email, [FromBody]string password)
        {
            IEnumerable<UserDTO> allUsers = await userRepository.GetAllAsync();
            UserDTO user = allUsers.First(u => u.Email == email);

            string calculatedHash = passwordLogic.HashPassword(password, user.PasswordSalt);
            bool isMatching = user.PasswordHash == calculatedHash;

            return new UserVerificationResult
            {
                Id = user.Id,
                IsVerifiedAsPositive = isMatching && user.IsActive,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
