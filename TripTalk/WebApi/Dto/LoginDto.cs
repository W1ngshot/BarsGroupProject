using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto;

public class LoginDto
{
    [Required]
    public string Nickname { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}