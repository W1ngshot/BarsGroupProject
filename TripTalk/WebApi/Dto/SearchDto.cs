using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto;

public class SearchDto
{
    [Required]
    public string Text { get; set; } = string.Empty;

    public List<string>? Tags { get; set; } = null;

    public int PageNumber { get; set; } = 1;
}
