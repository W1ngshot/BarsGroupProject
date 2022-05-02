using System.ComponentModel.DataAnnotations;

namespace OldWeb.Dto;

public class LoginDto
{
    [Required(ErrorMessage = "Не указан Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}