using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DbModels;

public class ArticleDbModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? ShortDescription { get; set; }
    public string Text { get; set; } = null!;
    public DateTime UploadDate { get; set; }
    public UserDbModel User { get; set; } = null!;
    public int UserId { get; set; }
    public string? AssetLink { get; set; }
    public int Views { get; set; }
    public List<TagDbModel> Tags { get; set; } = new();
    public List<CommentDbModel> Comments { get; set; } = new();
    public List<RateDbModel> Rates { get; set; } = new();


    internal class Map : IEntityTypeConfiguration<ArticleDbModel>
    {
        public void Configure(EntityTypeBuilder<ArticleDbModel> builder)
        {
            builder.ToTable("article");

            builder.HasKey(article => article.Id)
                .HasName("pk_article");

            builder.Property(article => article.Title)
                .IsRequired();

            builder.Property(article => article.Text)
                .IsRequired();

            builder.Property(article => article.UploadDate)
                .IsRequired();

            builder.HasOne(article => article.User)
                .WithMany(user => user.Articles)
                .HasForeignKey(article => article.UserId)
                .HasConstraintName("fk_user_id");

            builder.Property(article => article.Views)
                .IsRequired()
                .HasDefaultValue(0);
        }
    }
}