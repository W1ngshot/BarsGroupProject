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

    public const string EmptyArticleTitle = "Введите название статьи";
    public const string TooShortArticleTitle = "Слишком короткое название";
    public const string TooLongArticleTitle = "Слишком длинное название";
    public const string ArticleTitleContainsWrongSymbols = "Недопустимые символы в названии";
    public const string TooLongArticleDescription = "Слишком длинное описание";
    public const string EmptyArticleText = "Введите текст статьи";
    public const string TooShortArticleText = "Текст статьи должен содержать не менее 500 символов";
    public const string TooLongArticleText = "Текст статьи должен содержать не более 10 тысяч символов";
}
