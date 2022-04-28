using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DbModels;

public class CommentDbModel
{
    public int Id { get; set; }
    public string Message { get; set; } = null!;
    public DateTime Date { get; set; }
    public UserDbModel User { get; set; } = null!;
    public int UserId { get; set; }
    public ArticleDbModel Article { get; set; } = null!;
    public int ArticleId { get; set; }


    internal class Map : IEntityTypeConfiguration<CommentDbModel>
    {
        public void Configure(EntityTypeBuilder<CommentDbModel> builder)
        {
            builder.ToTable("comment");

            builder.HasKey(comment => comment.Id)
                .HasName("pk_comment");

            builder.Property(comment => comment.Message)
                .IsRequired();

            builder.Property(comment => comment.Date)
                .IsRequired();

            builder.HasOne(comment => comment.User)
                .WithMany()
                .HasForeignKey(comment => comment.UserId)
                .HasConstraintName("fk_user_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(comment => comment.Article)
                .WithMany(article => article.Comments)
                .HasConstraintName("fk_article_id")
                .HasForeignKey(comment => comment.ArticleId);
        }
    }
}