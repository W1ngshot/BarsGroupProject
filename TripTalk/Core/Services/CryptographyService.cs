using Konscious.Security.Cryptography;
using System.Text;

namespace Core.Services
{
    public class CryptographyService : ICryptographyService
    {
        public async Task<string> EncryptPasswordAsync(string password, byte[] salt)
        {
            var encrypter = new Argon2d(Encoding.Unicode.GetBytes(password));

            encrypter.Salt = salt;
            encrypter.Iterations = 40;
            encrypter.DegreeOfParallelism = 16;
            encrypter.MemorySize = 8192;

            var encodedBytes = await encrypter.GetBytesAsync(128);
            return Convert.ToBase64String(encodedBytes);
        }
    }
}