using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImageLibrary.Database.Models;

public class Group
{
    [Key]
    public int? Id { get; set; }
    public string Name { get; set; }
    public int? ThumbnailId { get; set; }
    public string Sequence { get; set; }
    
    //TODO: could be added enum later
    //public int Type;

    public List<Image> GroupImages;

    public Group(int? id, string name, int? thumbnailId, string sequence)
    {
        Id = id;
        Name = name;
        ThumbnailId = thumbnailId;
        GroupImages = new List<Image>();
        Sequence = sequence;
    }

    public Group(int id, string name, int thumbnailId)
    {
        Id = id;
        Name = name;
        ThumbnailId = thumbnailId;
        GroupImages = new List<Image>();
        Sequence = "";
    }

    public Group(string name, int thumbnailId)
    {
        Id = null;
        Name = name;
        ThumbnailId = thumbnailId;
        GroupImages = new List<Image>();
        Sequence = "";
    }

    public Group(string name)
    {
        Id = null;
        Name = name;
        ThumbnailId = null;
        GroupImages = new List<Image>();
        Sequence = "";
    }
}
