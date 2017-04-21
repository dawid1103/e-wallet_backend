using EwalletCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

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
    }
}
