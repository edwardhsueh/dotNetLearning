using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Xml.Linq;
using static System.Convert;
namespace Packt.Shared
{
    public class Protector
    {
        // salt size must be at least 8 bytes, we will use 16 bytes
        // unicode use UTF-16 encoding, and each ascill will be represented by 2bytes
        private static readonly byte[] salt = Encoding.Unicode.GetBytes("7BANANAS");
        private static readonly int iterations = 2000;
        private static Dictionary<string, User> Users = new Dictionary<string, User>();
        /// <summary>
        /// Encrypt function
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Encrypt(string plainText, string password)
        {
            byte[] encryptedBytes;
            byte[] plainBytes = Encoding.Unicode.GetBytes(plainText);
            Aes aes = Aes.Create(); // abstract class factory method
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = pbkdf2.GetBytes(32); // set a 256-bit key
            aes.IV = pbkdf2.GetBytes(16); // set a 128-bit IV
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(),CryptoStreamMode.Write))
                {
                    cs.Write(plainBytes, 0, plainBytes.Length);
                }
                encryptedBytes = ms.ToArray();
            }
            return Convert.ToBase64String(encryptedBytes);
        }
        /// <summary>
        /// Decrypt function
        /// </summary>
        /// <param name="cryptoText"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Decrypt(string cryptoText, string password)
        {
            byte[] plainBytes;
            byte[] cryptoBytes = Convert.FromBase64String(cryptoText);
            var aes = Aes.Create();
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = pbkdf2.GetBytes(32);
            aes.IV = pbkdf2.GetBytes(16);
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cryptoBytes, 0, cryptoBytes.Length);
                }
                plainBytes = ms.ToArray();
            }
            return Encoding.Unicode.GetString(plainBytes);
        }
        /// <summary>
        /// Register flow: using SaltAndHashPassword to generated saltedPassword and store salt in Dictionary
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static User Register(
            string username, string password,
            string[] roles = null)
        {
            byte[] saltBytes = new byte[16];
            //RNGCryptoServiceProvider is an implementation of a random number generator.
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            // The array is now filled with cryptographically strong random bytes.
            rng.GetBytes(saltBytes);
            var saltText = Convert.ToBase64String(saltBytes);
            // generate the salted and hashed password
            var saltedhashedPassword = SaltAndHashPassword(password, saltText);
            var user = new User
            {
                Name = username, Salt = saltText,
                SaltedHashedPassword = saltedhashedPassword,
                Roles = roles,
            };
            try{
                Users.Add(user.Name, user);
                return user;
            }
            catch(Exception ex){
                Console.WriteLine("{0} says {1}", ex.GetType(), ex.Message);
                return user;
            }
        }
        public static void LogIn(string username, string password)
        {
            if (CheckPassword(username, password))
            {
                var identity = new GenericIdentity(name: username, type: "PacktAuth");
                // return a GenericPrincipal
                var principal = new GenericPrincipal(identity: identity, roles: Users[username].Roles);
                System.Threading.Thread.CurrentPrincipal = principal;
            }
        }
        public static bool CheckPassword(string username, string password)
        {
            if (!Users.ContainsKey(username))
            {
                return false;
            }
            var user = Users[username];
            // re-generate the salted and hashed password
            var saltedhashedPassword = SaltAndHashPassword(password, user.Salt);
            return (saltedhashedPassword == user.SaltedHashedPassword);
        }

        /// <summary>
        /// using SHA256 to generate Hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private static string SaltAndHashPassword(string password, string salt)
        {
            var sha = SHA256.Create();
            var saltedPassword = password + salt;
            // computeHash return byte[] and convert to base64String
            return Convert.ToBase64String(
            sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));
        }

        public static byte[] GetRandomKeyOrIV(int size)
        {
            using (var r = RandomNumberGenerator.Create()){
                var data = new byte[size];
                r.GetNonZeroBytes(data);
                // data is an array now filled with
                // cryptographically strong random bytes
                return data;
            }
        }
    }
}
