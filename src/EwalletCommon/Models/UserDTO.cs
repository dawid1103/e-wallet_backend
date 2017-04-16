using EwalletCommon.Enums;
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
        public int Id { get; set; }

        /// <summary>
        /// User email which is also unique user login
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User hashed password
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Salt
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// User role e.g StandardUser, Admin
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// Modified date
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Inserted date - registration date
        /// </summary>
        public DateTime InsertedDate { get; set; }

        /// <summary>
        /// Is user active
        /// </summary>
        public bool IsActive { get; set; }
    }
}
