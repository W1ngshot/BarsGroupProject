namespace Core.Domains.User;

public class User
{
    public int Id { get; set; }
    public string Nickname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PasswordSalt { get; set; } = null!;
    public string? AvatarLink { get; set; }
    public DateTime RegistrationDate { get; set; }
}