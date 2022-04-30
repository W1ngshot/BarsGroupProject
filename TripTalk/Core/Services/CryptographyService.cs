using System.Security.Cryptography;
using System.Text;

using Konscious.Security.Cryptography;

namespace Core.Services;

public class CryptographyService : ICryptographyService
{
    public async Task<string> EncryptPasswordAsync(string password, string salt)
    {
        var encrypter = new Argon2d(Encoding.Unicode.GetBytes(password));

        encrypter.Salt = Convert.FromBase64String(salt);
        encrypter.Iterations = 40;
        encrypter.DegreeOfParallelism = 16;
        encrypter.MemorySize = 8192;

        var encodedBytes = await encrypter.GetBytesAsync(128);
        return Convert.ToBase64String(encodedBytes);
    }

    public string GenerateSalt()
    {
        var saltBytes = new byte[16];
        using (var cryptoRNG = RandomNumberGenerator.Create())
        {
            cryptoRNG.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }
}