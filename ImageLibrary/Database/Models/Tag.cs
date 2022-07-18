using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageLibrary.Database.Models;

public class Tag
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    public int? TagMetaTypeId { get; set; }
    public TagMetaType? TagMetaType { get; set; }

    public List<Image> Images { get; set; }


    public Tag(string name, string description = "")
    {
        Name = name;
        Description = description;
    }

    public Tag(string name, string description = "", int? tagMetaType = null)
    {
        Name = name;
        Description = description;
        TagMetaTypeId = tagMetaType;
    }

}
