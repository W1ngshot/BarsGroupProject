using Data.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.Db;

public class TripTalkContext : DbContext
{
    public DbSet<UserDbModel> Users { get; set; }
    public DbSet<ArticleDbModel> Articles { get; set; }
    public DbSet<CommentDbModel> Comments { get; set; }
    public DbSet<TagDbModel> Tags { get; set; }
    public DbSet<RateDbModel> Rates { get; set; }

    public TripTalkContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TripTalkContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

public class Factory : IDesignTimeDbContextFactory<TripTalkContext>
{
    public TripTalkContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder()
            .UseNpgsql("FakeConnectionStringOnlyForMigrations")
            .UseSnakeCaseNamingConvention()
            .Options;
        return new TripTalkContext(options);
    }
}