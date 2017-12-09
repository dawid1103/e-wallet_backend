using EwalletCommon.Enums;
using System;

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
        /// User hashed password
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Salt
        /// </summary>
        public string Salt { get; set; }

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

        public UserDTO() { }

        /// <summary>
        /// Create new user with all needed data
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="salt">Salt to hash with password</param>
        /// <param name="passwordHash">Hashed password</param>
        public UserDTO(string email, string salt, string passwordHash)
        {
            Email = email;
            Salt = salt;
            PasswordHash = passwordHash;
            Role = UserRole.Admin;
            IsActive = true;
        }
    }
}
