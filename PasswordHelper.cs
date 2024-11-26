using System;
using System.Security.Cryptography;
using System.Text;

namespace Fitness_Diary.ContentPages
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password, out byte[] salt)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                salt = new byte[16];
                rng.GetBytes(salt);
            }

            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = Combine(salt, Encoding.UTF8.GetBytes(password));
                var hash = sha256.ComputeHash(saltedPassword);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(string enteredPassword, byte[] salt, string storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = Combine(salt, Encoding.UTF8.GetBytes(enteredPassword));
                var hash = sha256.ComputeHash(saltedPassword);
                var enteredHash = Convert.ToBase64String(hash);
                return storedHash == enteredHash;
            }
        }

        private static byte[] Combine(byte[] salt, byte[] password)
        {
            var combined = new byte[salt.Length + password.Length];
            Buffer.BlockCopy(salt, 0, combined, 0, salt.Length);
            Buffer.BlockCopy(password, 0, combined, salt.Length, password.Length);
            return combined;
        }
    }
}
