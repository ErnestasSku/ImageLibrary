using ImageLibrary.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageLibrary.Database;

public class LibraryDbContext : DbContext
{
    public DbSet<Library> Librarys { get; set; }
        
    public LibraryDbContext()
    {
        
    }

    //public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base (options)
    //{
    //}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data source=library.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Library>()
            .HasKey(k => k.LibraryId);
    }
}