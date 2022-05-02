namespace WebApi.Dto;

public class ArticleDto
{
    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string? PictureLink { get; set; }
}