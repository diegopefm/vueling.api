using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Vueling.Api.Helpers
{
    public class Crypto
    {
        public static string GetSHA1(string str)
        {
            SHA1 sha1 = SHA1.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public static string AES_encrypt(string plain, string secretKey, string initVec)
        {
            using (Aes aes = new AesManaged())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.KeySize = 128;                              // in bits
                aes.Key = Encoding.UTF8.GetBytes(secretKey);    // new byte[128 / 8];  // 16 bytes for 128 bit encryption
                aes.IV = Encoding.UTF8.GetBytes(initVec);       // AES needs a 16-byte IV

                byte[] cipherText = null;
                byte[] plainText = Encoding.ASCII.GetBytes(plain);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plainText, 0, plainText.Length);
                    }

                    cipherText = ms.ToArray();
                }

                return Convert.ToBase64String(cipherText);
            }
        }

        public static string AES_decrypt(string encrypted, string secretKey, string initVec)
        {
            string decrypted = "";

            using (Aes aes = new AesManaged())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.KeySize = 128;                              // in bits
                aes.Key = Encoding.UTF8.GetBytes(secretKey);    // new byte[128 / 8];  // 16 bytes for 128 bit encryption
                aes.IV = Encoding.UTF8.GetBytes(initVec);       // AES needs a 16-byte IV

                byte[] cipherText = Convert.FromBase64String(encrypted);
                byte[] plainText = null;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherText, 0, cipherText.Length);
                    }

                    plainText = ms.ToArray();
                }
                decrypted = Encoding.ASCII.GetString(plainText);
            }

            return decrypted;
        }
    }
}