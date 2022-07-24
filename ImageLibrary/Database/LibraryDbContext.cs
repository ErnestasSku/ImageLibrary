using ImageLibrary.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageLibrary.Database;

/// <summary>
/// Database context for a list of libraries.
/// </summary>
public class LibraryDbContext : DbContext
{
    public DbSet<Library> Librarys { get; set; }
        
    public LibraryDbContext()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data source=library.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Library>()
            .HasKey(k => k.LibraryId);
        
        modelBuilder.Entity<Library>()
            .Property(f => f.LibraryId)
            .ValueGeneratedOnAdd();
    }
}