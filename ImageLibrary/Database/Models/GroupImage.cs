using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageLibrary.Database.Models;

public class GroupImage
{
    public int ImageId { get; set; }
    public Image Image { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; }

}
