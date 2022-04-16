using System.ComponentModel.DataAnnotations;

namespace Web.Dto;

public class ChangePasswordDto
{
    [Required]
    [DataType(DataType.Password)]
    public string OldPassword { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароль введен неверно")]
    public string NewPassword { get; set; }
}