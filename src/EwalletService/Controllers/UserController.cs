using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EwalletService.Logic;
using EwalletCommon.Models;
using EwalletService.Repository;
using EwalletService.Models;
using EwalletCommon.Enums;

namespace EwalletService.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IPasswordLogic _passwordLogic;
        private readonly IUserRepository _userRepository;

        public UserController(IPasswordLogic passwordLogic, IUserRepository userRepository)
        {
            _passwordLogic = passwordLogic;
            _userRepository = userRepository;
        }


        [HttpPost]
        public async Task<int> CreateAsync([FromBody]UserDTO user)
        {
            UserCredentials credentials = _passwordLogic.HashPassword(user.Password);
            user.PasswordHash = credentials.Hash;
            user.PasswordSalt = credentials.Salt;

            //temporary
            user.Role = UserRole.Admin;

            int userId = await _userRepository.CreateAsync(user);
            return userId;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<UserDTO> GetAsync(int id)
        {
            return await _userRepository.GetAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        //TODO later
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]UserDTO user)
        //{
        //}

        [HttpPost("verifyuser")]
        public async Task<UserVerificationResult> VerifyUser([FromQuery]string email, [FromBody]string password)
        {
            IEnumerable<UserDTO> allUsers = await _userRepository.GetAllAsync();
            UserDTO user = allUsers.First(u => u.Email == email);

            string calculatedHash = _passwordLogic.HashPassword(password, user.PasswordSalt);
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
