using System.Security.Cryptography;
using System.Text;
using CarRentalAPI.Helpers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


// EVERYTHING IN THIS FILE IS ARDA'S CODE, committed by alex because of merge issues
namespace CarRentalAPI.Helpers
{
    public static class EncryptionHelper
    {
        private static readonly string EncryptionKey = "TanushBoy"; 

        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            // Nothing to encrypt
            return plainText; 

            using (Aes aes = Aes.Create())
            {   // 256-bit key
                var keyBytes = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32)); 
                aes.Key = keyBytes;
                aes.IV = new byte[16];

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cryptoStream))
                    {
                        writer.Write(plainText);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
            // Nothing to decrypt
            return cipherText; 

            using (Aes aes = Aes.Create())
            {
                var keyBytes = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32)); 
                aes.Key = keyBytes;
                aes.IV = new byte[16];

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                using (var cryptoStream = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var reader = new StreamReader(cryptoStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}


// converter class to handle automatic encryption and decryption when interacting with DB
public class EncryptedStringConverter : ValueConverter<string?, string?>
{
    public EncryptedStringConverter()
        : base(
            plain => plain == null ? null : EncryptionHelper.Encrypt(plain),
            encrypted => encrypted == null ? null : EncryptionHelper.Decrypt(encrypted))
    { }
}


