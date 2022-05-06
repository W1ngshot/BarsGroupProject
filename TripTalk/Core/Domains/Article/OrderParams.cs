namespace Core.Domains.Article;

public enum Category
{
    Popular,
    Last,
    Best
}

public enum Period
{
    Today = 1,
    LastWeek = 7,
    LastMonth = 30,
    AllTime = int.MaxValue
}