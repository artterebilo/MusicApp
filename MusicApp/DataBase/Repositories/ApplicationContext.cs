using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Repositories;

public class ApplicationContext : DbContext
{
    public DbSet<ArtistModel> Artists { get; set; } = null!;
    public DbSet<AlbumModel> Albums { get; set; } = null!;
    public DbSet<SongModel> Songs { get; set; } = null!;
    public DbSet<UserModel> Users { get; set; } = null!;
    public DbSet<UserLikeSongsModel> UsersLikesSongs { get; set; } = null!;

    public string connectionString = "Data Source=WATRUSHECHKA;Database=MusicApp;User Id=sa;Password=34172839;TrustServerCertificate=True";
    public ApplicationContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserLikeSongsModel>()
            .HasKey(e => new { e.UserId, e.SongId});
    }
}
