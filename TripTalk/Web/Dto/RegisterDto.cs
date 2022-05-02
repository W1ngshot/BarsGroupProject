using System.ComponentModel.DataAnnotations;

namespace OldWeb.Dto;

public class RegisterDto
{
    [Required(ErrorMessage = "Не указан никнейм")]
    public string Nickname { get; set; }

    [Required(ErrorMessage = "Не указан Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Введите пароль повторно")]
    [Compare("Password", ErrorMessage = "Пароль введен неверно")]
    public string ConfirmPassword { get; set; }
}