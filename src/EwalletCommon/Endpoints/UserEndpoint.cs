using EwalletCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace EwalletCommon.Endpoints
{
    public class UserEndpoint : ServiceEndpoint
    {
        public UserEndpoint(HttpClient client) : base(client) { }


        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user">UserDTO object</param>
        /// <returns>User id of created</returns>
        public async Task<int> CreateAsync(UserDTO user)
        {
            return await PostAsync<int>("user", user);
        }

        /// <summary>
        /// Delete user of given user id
        /// </summary>
        /// <param name="id">User id</param>
        public async Task DeleteAsync(int id)
        {
            await DeleteAsync($"user/{id}");
        }

        /// <summary>
        /// Returns UserDTO object of user with given user id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>UserDTO object</returns>
        public async Task<UserDTO> GetAsync(int id)
        {
            return await base.GetAsync<UserDTO>($"user/{id}");
        }

        /// <summary>
        /// Returns all users
        /// </summary>
        /// <returns>IEnumerable<UserDTO></returns>
        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            return await base.GetAsync<IEnumerable<UserDTO>>("user");
        }

        /// <summary>
        /// Verify user if exist in system
        /// </summary>
        /// <param name="email">User email/login</param>
        /// <param name="password">User password</param>
        /// <returns>
        /// UserVerificationResult{
        ///     userId,
        ///     userEmail/Login,
        ///     role,
        ///     result of verification
        /// }
        /// </returns>
        public async Task<UserVerificationResult> VerifyUser(string email, string password)
        {
            string requestUrl = $"user/verifyuser?email={WebUtility.UrlEncode(email)}";
            var verifycationResult = await PostAsync<UserVerificationResult>(requestUrl, password);

            return verifycationResult;

        }
    }
}
