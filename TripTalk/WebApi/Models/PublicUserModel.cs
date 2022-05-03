namespace WebApi.Models;

public class PublicUserModel
{
    public int Id { get; set; }
    public string Nickname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? AvatarLink { get; set; }
    public DateTime RegistrationDate { get; set; }
}