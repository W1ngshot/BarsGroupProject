namespace Core.Models;

public class Comment
{
    public int Id { get; set; }
    public string Message { get; set; } = null!;
    public DateTime Date { get; set; }
    public int UserId { get; set; }
    public int ArticleId { get; set; }
}