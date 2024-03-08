using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repositories;

public class ApplicationContext : DbContext
{
    public DbSet<ArtistModel> Artists { get; set; } = null!;
    public DbSet<AlbumModel> Albums { get; set; } = null!;

    public string connectionString = "Data Source=WATRUSHECHKA;Database=MusicApp;User Id=sa;Password=34172839;TrustServerCertificate=True";
    //public string connectionString = @"Server=WATRUSHECHKA;Database=MusicApp;Trusted_Connection=True;";
    public ApplicationContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }
}
