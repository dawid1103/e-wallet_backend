using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EwalletServices.Logic;
using EwalletCommon.Models;
using EwalletServices.Repository;
using EwalletServices.Models;
using EwalletCommon.Enums;

namespace EwalletServices.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IPasswordLogic _passwordLogic;
        private readonly IUserRepository _userRepositiry;

        public UserController(IPasswordLogic passwordLogic, IUserRepository userRepository)
        {
            _passwordLogic = passwordLogic;
            _userRepositiry = userRepository;
        }


        [HttpPost]
        public async Task<int> CreateAsync([FromBody]UserDTO user)
        {
            UserCredentials credentials = _passwordLogic.HashPassword(user.Password);
            user.PasswordHash = credentials.Hash;
            user.PasswordSalt = credentials.Salt;

            //temporary
            user.Role = UserRole.Admin;

            int userId = await _userRepositiry.AddAsync(user);
            return userId;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]UserDTO user)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
