using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DbModels;

public class TagDbModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<ArticleDbModel>? Articles { get; set; }


    internal class Map : IEntityTypeConfiguration<TagDbModel>
    {
        public void Configure(EntityTypeBuilder<TagDbModel> builder)
        {
            builder.ToTable("tag");

            builder.HasKey(tag => tag.Id)
                .HasName("pk_tag");

            builder.HasAlternateKey(tag => tag.Name)
                .HasName("ak_name");

            builder.Property(tag => tag.Name)
                .IsRequired();

            builder.HasMany(tag => tag.Articles)
                .WithMany(article => article.Tags)
                .UsingEntity(builder => builder.ToTable("attached_tags"));
        }
    }
}