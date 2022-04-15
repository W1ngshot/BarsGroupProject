using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Data.DbModels;

public class TagDbModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ArticleId { get; set; }
    public ArticleDbModel Article { get; set; }


    internal class Map : IEntityTypeConfiguration<TagDbModel>
    {
        public void Configure(EntityTypeBuilder<TagDbModel> builder)
        {
            builder.ToTable("tag");

            builder.HasKey(tag => tag.Id)
                .HasName("pk_tag");

            builder.Property(tag => tag.Name)
                .IsRequired();

            builder.HasOne(tag => tag.Article)
                .WithMany()
                .HasForeignKey(article => article.ArticleId)
                .HasConstraintName("fk_article_id");
        }
    }
}