using EwalletService.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace EwalletService.Logic
{
    public interface IPasswordLogic
    {
        string HashPassword(string password, string salt);
        UserCredentials HashPassword(string password);
    }
    public class PasswordLogic : IPasswordLogic
    {
        public static string HashFunction(string password, byte[] salt)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hash;
        }

        public UserCredentials HashPassword(string password)
        {
            // generate a 256-bit salt using a secure PRNG
            byte[] salt = new byte[256 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = HashFunction(password, salt);

            return new UserCredentials
            {
                Hash = hashed,
                Salt = Convert.ToBase64String(salt)
            };
        }

        public string HashPassword(string password, string salt)
        {
            byte[] saltByteArray = Convert.FromBase64String(salt);
            string hashed = HashFunction(password, saltByteArray);

            return hashed;
        }
    }
}
