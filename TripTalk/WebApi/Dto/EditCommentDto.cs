using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto;

public class EditCommentDto
{
    [Required]
    public int CommentId { get; set; }

    [Required]
    public string Message { get; set; } = string.Empty;
}