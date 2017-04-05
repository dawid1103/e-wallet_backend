using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletCommon.Models
{
    public class UserDTO
    {
        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User email which is also user login
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }
    }
}
