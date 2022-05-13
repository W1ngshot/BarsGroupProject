namespace Core.Domains.User;

public class SetPassword
{
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}