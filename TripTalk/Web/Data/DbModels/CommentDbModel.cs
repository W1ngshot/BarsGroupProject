using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Data.DbModels;

public class CommentDbModel
{
    public int Id { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
    public int UserId { get; set; }
    public UserDbModel User { get; set; }
    public int ArticleId { get; set; }
    public ArticleDbModel Article { get; set; }


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
                .WithMany()
                .HasConstraintName("fk_article_id")
                .HasForeignKey(comment => comment.ArticleId);
        }
    }
}