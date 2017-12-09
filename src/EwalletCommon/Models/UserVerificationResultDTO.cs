using EwalletCommon.Enums;

namespace EwalletCommon.Models
{
    public class UserVerificationResultDTO
    {
        /// <summary>
        /// User id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// True if user has an account and the credentials match 
        /// </summary>
        public bool IsVerifiedAsPositive { get; set; }

        /// <summary>
        /// User unique email and also user name
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        public UserRole Role { get; set; }
    }
}
