using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto;

public class AddCommentDto
{
    [Required]
    public string Message { get; set; } = string.Empty;

    [Required]
    public int ArticleId { get; set; }
}