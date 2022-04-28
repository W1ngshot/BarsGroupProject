using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DbModels;

public class UserDbModel
{
    public int Id { get; set; }
    public string Nickname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PasswordSalt { get; set; } = null!;
    public string? AvatarLink { get; set; }
    public DateTime RegistrationDate { get; set; }
    public List<ArticleDbModel>? Articles { get; set; }

    internal class Map : IEntityTypeConfiguration<UserDbModel>
    {
        public void Configure(EntityTypeBuilder<UserDbModel> builder)
        {
            builder.ToTable("user");

            builder.HasKey(user => user.Id)
                .HasName("pk_user");

            builder.Property(user => user.Nickname)
                .IsRequired();

            builder.Property(user => user.Email)
                .IsRequired();

            builder.Property(user => user.PasswordHash)
                .IsRequired()
                .HasDefaultValue("hash");

            builder.Property(user => user.PasswordSalt)
                .IsRequired()
                .HasDefaultValue("salt");

            builder.Property(user => user.RegistrationDate)
                .IsRequired();
        }
    }
}