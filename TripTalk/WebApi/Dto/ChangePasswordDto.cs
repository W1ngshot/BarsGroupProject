using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto;

public class ChangePasswordDto
{
    [Required]
    public string OldPassword { get; set; } = string.Empty;

    [Required]
    public string NewPassword { get; set; } = string.Empty;

    [Required]
    public string ConfirmNewPassword { get; set; } = string.Empty;
}