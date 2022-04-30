namespace Core.Models;

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? ShortDescription { get; set; }
    public string Text { get; set; } = null!;
    public DateTime UploadDate { get; set; }
    public int UserId { get; set; }
    public string? PreviewPictureLink { get; set; }
    public List<string>? AttachedPicturesLinks { get; set; }
    public int Rating { get; set; }
    public int Views { get; set; }
    public string UserNickname { get; set; }
    public List<string> TagNames { get; set; } = new();
}