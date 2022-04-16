namespace Core.Models;

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? ShortDescription { get; set; }
    public string Text { get; set; }
    public string? PictureLink { get; set; }
    public DateTime UploadDate { get; set; }
    public int UserId { get; set; }
}