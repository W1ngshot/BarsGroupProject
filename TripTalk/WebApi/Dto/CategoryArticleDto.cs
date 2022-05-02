using Core;

namespace WebApi.Dto;

public class CategoryArticleDto
{
    public Period Period { get; set; }
    public int PageNumber { get; set; } = 1;
}