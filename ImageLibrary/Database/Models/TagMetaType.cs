using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageLibrary.Database.Models;

public class TagMetaType
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Background { get; set; } = "FFFFFF";
    public string Foreground { get; set; } = "000000";

    public List<Tag> Tags { get; set; }
}
