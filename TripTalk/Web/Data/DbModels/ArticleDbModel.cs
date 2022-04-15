using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Data.DbModels;

public class ArticleDbModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? ShortDescription { get; set; }
    public string Text { get; set; }
    public DateTime UploadDate { get; set; }
    public int UserId { get; set; }
    public UserDbModel User { get; set; }
    public string? AssetLink { get; set; }
    //public List<TagDbModel> Tags { get; set; }
    //public List<CommentDbModel> Comments { get; set; }
    //public List<RateDbModel> Rates { get; set; }


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
                .WithMany()
                .HasForeignKey(article => article.UserId);
        }
    }
}