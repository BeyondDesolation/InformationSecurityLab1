using D.IBlab1.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Security.Cryptography;
using System;
using System.Text;
using System.Linq;

namespace D.IBlab1.Services
{
    internal static class DataFileService
    {
        public static void SerializeToFile(string filePath, IEnumerable<User> data)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, data);
            }
        } 

        public static IEnumerable<User> DeserializeFromFile(string filePath)
        {
            if(File.Exists(filePath) == false)
               return GetDefaultList();

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                var data = JsonSerializer.Deserialize<IEnumerable<User>>(fs);
                return data ?? GetDefaultList();
            }
        }

        public static void EncryptToFile(string key, string filePath, IEnumerable<User> data)
        {
            var dataArray = JsonSerializer.SerializeToUtf8Bytes(data);

            SHA512 sha = SHA512.Create();
            var keyHash = sha.ComputeHash(Encoding.UTF8.GetBytes(key)).Take(8).ToArray();
            
            using (DES des1 = DES.Create())
            {
                des1.GenerateIV();
                var IV = des1.IV;
                des1.Key = keyHash;
                var encrypted = des1.EncryptCbc(dataArray, IV);

                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    fs.Write(IV, 0, IV.Length);
                    fs.Write(encrypted, 0, encrypted.Length);
                }
            }
        }

        public static IEnumerable<User> DecryptFromFile(string key, string filePath)
        {
            if (File.Exists(filePath) == false)
                return GetDefaultList();


            SHA512 sha = SHA512.Create();
            var keyHash = sha.ComputeHash(Encoding.UTF8.GetBytes(key)).Take(8).ToArray();

            using (DES des1 = DES.Create())
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    var IV = new byte[des1.IV.Length];
                    var cipherText = new byte[buffer.Length - IV.Length];

                    Array.Copy(buffer, IV, IV.Length);
                    Array.Copy(buffer, IV.Length, cipherText, 0, cipherText.Length);

                    des1.Key = keyHash;

                    var decrypted = des1.DecryptCbc(cipherText, IV);

                    return JsonSerializer.Deserialize<IEnumerable<User>>(decrypted);
                }
            }
        }
        private static IEnumerable<User> GetDefaultList()
        {
            return new List<User>() { new User { Login = "admin", Role = 0 } };
        }
    }
}
