using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace SmartTech.Utilities
{
    public class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 10000;

        public static string HashPassword(string password)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SaltSize];
                rng.GetBytes(salt);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
                {
                    byte[] key = pbkdf2.GetBytes(KeySize);
                    byte[] hashBytes = new byte[SaltSize + KeySize];

                    Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                    Array.Copy(key, 0, hashBytes, SaltSize, KeySize);

                    return Convert.ToBase64String(hashBytes);
                }
            }
        }
        public static bool ValidatePassword(string password, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                byte[] key = pbkdf2.GetBytes(KeySize);
                for (int i = 0; i < KeySize; i++)
                {
                    if (hashBytes[i + SaltSize] != key[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}