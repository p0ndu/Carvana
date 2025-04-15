using System.Security.Cryptography;
using System.Text;
using CarRentalAPI.Helpers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarRentalAPI.Helpers
{
    public static class HashHelper
    {
        private const int _saltSize = 16; // 128 bits
        private const int _hashSize = 32; // 256 bits
        private const int _iterations = 10000;
        
        // Hashes a password using randomised salt and returns both as a string
        public static string HashPassword(string password)
        {
            string cypherPass;
            
            // Generate salt (random bytes used to hash)
            byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);

            // Hash password using pbkdf2 algo
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, HashAlgorithmName.SHA256, _hashSize);

            // Join the two using : and return
            string saltString = Convert.ToBase64String(salt);
            string hasString = Convert.ToBase64String(hash);
            cypherPass = saltString + ":" + hasString;

            return cypherPass;
        }

        // checks if password is equal to hashString when hashed using the same salt
        public static bool VerifyPassword(string password, string hashString)
        {
            bool match = false;
            
            // Separate salt and cypherPass
            string[] split = hashString.Split(':');
            byte[] salt = Convert.FromBase64String(split[0]);
            byte[] cypherPass = Convert.FromBase64String(split[1]);
            
            // Hash password
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, HashAlgorithmName.SHA256, _hashSize); 

            // Return comparison using built in function because this is due in 2 days and there is absolutely no way im doing it manually
            match = hash.SequenceEqual(cypherPass);
            return match;
        }
    }
}

