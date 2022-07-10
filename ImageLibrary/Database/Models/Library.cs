using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageLibrary.Database.Models
{
    public class Library
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LibraryId { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Library()
        {
            
        }

        public Library (int libraryId, string path, string name, string description = "")
        {
            LibraryId = libraryId;
            Path = path;
            Name = name;
            Description = description;
        }
    }
}
