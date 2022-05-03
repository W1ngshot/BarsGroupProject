using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto;

public class RegisterDto
{
    [Required]
    public string Nickname { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}