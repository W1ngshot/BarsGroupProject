using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DbModels;

public class ArticleTagDbModel
{
    public int Id { get; set; }
    public ArticleDbModel Article { get; set; } = null!;
    public int ArticleId { get; set; }
    public TagDbModel Tag { get; set; } = null!;
    public int TagId { get; set; }


    internal class Map : IEntityTypeConfiguration<ArticleTagDbModel>
    {
        public void Configure(EntityTypeBuilder<ArticleTagDbModel> builder)
        {
            builder.ToTable("article_tag");

            builder.HasKey(articleTag => articleTag.Id)
                .HasName("pk_article_tag");
        }
    }
}