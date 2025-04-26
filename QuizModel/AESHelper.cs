using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Quiz.Model
{
    public class AESHelper
    {
        private static readonly Encoding encoding = Encoding.UTF8;

        public static string Encrypt(string text, string key)
        {
            try
            {
                Aes aes = Aes.Create();
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                aes.Key = SHA256.Create().ComputeHash(encoding.GetBytes(key));
                aes.IV = encoding.GetBytes(key);

                var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);

                var bufferText = encoding.GetBytes(text);
                var encryptedText =
                    Convert.ToBase64String(encrypt.TransformFinalBlock(bufferText, 0, bufferText.Length));

                return encryptedText;
            }
            catch (Exception e)
            {
                throw new Exception("Error encrypting: " + e.Message);
            }
        }

        public static string Decrypt(string text, string key)
        {
            try
            {
                Aes aes = Aes.Create(); ;
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                aes.Key = SHA256.Create().ComputeHash(encoding.GetBytes(key));
                aes.IV = encoding.GetBytes(key);

                var decrypt = aes.CreateDecryptor(aes.Key, aes.IV);
                var bufferText = Convert.FromBase64String(text);

                return encoding.GetString(decrypt.TransformFinalBlock(bufferText, 0, bufferText.Length));
            }
            catch (Exception e)
            {
                throw new Exception("Error decrypting: " + e.Message);
            }
        }
    }
}

