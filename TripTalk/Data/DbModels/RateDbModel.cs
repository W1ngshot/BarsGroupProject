using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DbModels;

public class RateDbModel
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public UserDbModel User { get; set; } = null!;
    public int UserId { get; set; }
    public ArticleDbModel Article { get; set; } = null!;
    public int ArticleId { get; set; }


    internal class Map : IEntityTypeConfiguration<RateDbModel>
    {
        public void Configure(EntityTypeBuilder<RateDbModel> builder)
        {
            builder.ToTable("rate");

            builder.HasIndex(rate => rate.ArticleId);

            builder.HasKey(rate => rate.Id)
                .HasName("pk_rate");

            builder.Property(rate => rate.Rating)
                .IsRequired();

            builder.HasOne(rate => rate.User)
                .WithMany()
                .HasForeignKey(rate => rate.UserId)
                .HasConstraintName("fk_user_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rate => rate.Article)
                .WithMany(article => article.Rates)
                .HasForeignKey(rate => rate.ArticleId)
                .HasConstraintName("fk_article_id");
        }
    }
}