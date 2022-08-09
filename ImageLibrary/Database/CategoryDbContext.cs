using ImageLibrary.Database.Models;
using Microsoft.EntityFrameworkCore;
using ImageLibrary.Utilities;

namespace ImageLibrary.Database;

/// <summary>
/// Database context for a certain library (category).
/// </summary>
public class CategoryDbContext : DbContext
{
    string dbPath;

    DbSet<Image> Images { get; set; }
    DbSet<Tag> Tags { get; set; }
    DbSet<TagAssociation> TagAssociation { get; set; }
    DbSet<TagMetaType> TagMetaTypes { get; set; }
    DbSet<Group> Groups { get; set; }


    public CategoryDbContext(string dbPath)
    {
        this.dbPath = dbPath;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.DatabaseName(dbPath);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Set Image properties
        modelBuilder.Entity<Image>()
            .HasKey(i => i.Id);
        
        modelBuilder.Entity<Image>()
            .Property(i => i.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Image>()
            .Property(i => i.Path)
            .IsRequired();

        modelBuilder.Entity<Image>()
            .Property(i => i.Name)
            .IsRequired();

        //Set Image relations
        modelBuilder.Entity<Image>()
            .HasMany(i => i.Tags)
            .WithMany(t => t.Images);
            //.UsingEntity<ImageTag>();

        modelBuilder.Entity<Image>()
            .HasOne(i => i.Group)
            .WithMany(g => g.GroupImages);
            //.UsingEntity<GroupImage>();

        // Set Group properties
        modelBuilder.Entity<Group>()
            .HasKey(g => g.Id);

        modelBuilder.Entity<Group>()
            .Property(g => g.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Group>()
            .Property(g => g.Name)
            .IsRequired();


        // Set Tag relations
        modelBuilder.Entity<Tag>()
            .HasKey(g => g.Id);

        modelBuilder.Entity<Tag>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<TagMetaType>()
            .HasKey(tmt => tmt.Id);

        modelBuilder.Entity<TagMetaType>()
            .HasMany(tmt => tmt.Tags)
            .WithOne(t => t.TagMetaType);
        
        modelBuilder.Entity<TagMetaType>()
            .Property(tmt => tmt.Background)
            .HasMaxLength(6);

        modelBuilder.Entity<TagMetaType>()
            .Property(tmt => tmt.Foreground)
            .HasMaxLength(6);


        // Set TagAssosiactions
        modelBuilder.Entity<TagAssociation>()
            .HasKey(ta => ta.AssosiactionId);
        
        modelBuilder.Entity<TagAssociation>()
            .Property(ta => ta.AssosiactionId)
            .ValueGeneratedOnAdd();

    }
}
