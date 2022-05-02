namespace WebApi.Dto;

public class SearchDto
{
    public string Text { get; set; } = null!;

    public List<string>? Tags { get; set; } = null;

    public int PageNumber { get; set; } = 1;
}
