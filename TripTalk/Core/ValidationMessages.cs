namespace Core;

public class ValidationMessages
{
    public const string EmptyLogin = "Поле логина не может быть пустым";
    public const string TooShortLogin = "Логин слишком короткий";
    public const string TooLongLogin = "Логин слишком длинный";
    public const string LoginContainsWrongSymbols = "Логин содержит недопустимые символы";
    public const string EmptyEmail = "Поле электронной почты не может быть пустым";
    public const string IncorrectEmail = "Почта введена неверно";
    public const string LoginOrEmailAlreadyExists = "Данный логин или почта уже были зарегистрированы";
}
