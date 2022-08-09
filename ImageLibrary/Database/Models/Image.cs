using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ImageLibrary.Database.Models;

public class Image
{
    [Key]
    public int? Id { get; set; }
    public string Path { get; set; }
    public string Name { get; set; }


    public List<Tag> Tags { get; set; }
    public Group? Group { get; set; }

    public Image(int id, string path, string name)
    {
        Id = id;
        Path = path;
        Name = name;
        Tags = new List<Tag>();
        Group = null;
    }

    public Image(string path, string name)
    {
        Id = null;
        Path = path;
        Name = name;
        Tags = new List<Tag>();
        Group = null;
    }

    public Image(string path)
    {
        Id = null;
        Path = path;
        FileInfo fileInfo = new FileInfo(Path);
        Name = fileInfo.Name;
        Tags = new List<Tag>();
        Group = null;
    }
}
