using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto;

public class ArticleDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    public string ShortDescription { get; set; } = string.Empty;

    [Required]
    public string Text { get; set; } = string.Empty;

    public string? PictureLink { get; set; }

    public List<string>? AttachedPicturesLinks { get; set; }
}