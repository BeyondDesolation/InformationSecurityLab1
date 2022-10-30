using System;
using System.Security.Cryptography;
using System.Text;

namespace D.IBlab1.Services
{
    internal static class PasswordHelperService
    {
        public static (string hashedPass, string salt) HashPassword(string password)
        {          
            var saltByteArray = RandomNumberGenerator.GetBytes(16);
            
            var hashedPassword = Hash(saltByteArray, password);

            return (Convert.ToBase64String(hashedPassword), Convert.ToBase64String(saltByteArray));
        }

        public static bool VerifyPassword(string hashedPassword, string salt, string password)
        {
            var saltByteArray = Convert.FromBase64String(salt);

            var hashedPassword1 = Hash(saltByteArray, password);

            var hashedPassword2 = Convert.FromBase64String(hashedPassword);

            for (int i = 0; i < hashedPassword1.Length; i++)
            {
                if (hashedPassword1[i] != hashedPassword2[i])
                    return false;
            }
            return true;
        }

        private static byte[] Hash(byte[] salt, string password)
        {
            // Вот тут должен быть нормальный безопасный алгоритм, а не вот это вот 
            var passwordArray = Encoding.UTF8.GetBytes(password);

            var saltPass = new byte[salt.Length + passwordArray.Length];

            Array.Copy(salt, 0, saltPass, 0, salt.Length);
            Array.Copy(passwordArray, 0, saltPass, salt.Length, passwordArray.Length);

            var hashedPassword = SHA512.Create().ComputeHash(saltPass);
            return hashedPassword;
        }
    }
}
