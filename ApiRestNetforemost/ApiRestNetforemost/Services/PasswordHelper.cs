using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace ApiRestNetforemost.Services
{
    public class PasswordHelper
    {
        public static string HashPassword(string password, byte[] salt)
        {
            var hashed = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32);

            var saltString = Convert.ToBase64String(salt);
            var hashString = Convert.ToBase64String(hashed);

            return $"{saltString}.{hashString}";
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            var parts = hashedPassword.Split('.');
            if (parts.Length != 2)
                throw new FormatException("El formato del hash es incorrecto.");

            var salt = Convert.FromBase64String(parts[0]);
            var hashStored = Convert.FromBase64String(parts[1]);

            var hashEntered = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32);

            return hashEntered.SequenceEqual(hashStored);
        }

        public static byte[] GenerateSalt()
        {
            var salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}
