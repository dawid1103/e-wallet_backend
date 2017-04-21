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
    }
}
