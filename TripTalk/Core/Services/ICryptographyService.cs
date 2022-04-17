namespace Core.Services;

public interface ICryptographyService
{
    public Task<string> EncryptPasswordAsync(string password, string salt);

    public string GenerateSalt();
}