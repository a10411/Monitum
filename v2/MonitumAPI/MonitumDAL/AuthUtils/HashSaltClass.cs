using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MonitumDAL.AuthUtils
{
    public class HashSaltClass
    {
        public static string GenerateSalt()
        {
            byte[] salt;
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt = new byte[16]);
            return Convert.ToBase64String(salt);
        }

        public static byte[] GetHash(string plainPassword, string Salt)
        {
            byte[] byteArray = Encoding.Unicode.GetBytes(String.Concat(Salt, plainPassword));
            using var sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(byteArray);
            return hashedBytes;
        }

        public static bool CompareHashedPasswords(string userInputPassword, string ExistingHashedBase64StringPassword, string SaltOnDB)
        {
            string UserInputHashedPassword = Convert.ToBase64String(GetHash(userInputPassword, SaltOnDB));
            return ExistingHashedBase64StringPassword == UserInputHashedPassword;
        }
    }
}
