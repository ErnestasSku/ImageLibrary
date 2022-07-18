using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageLibrary.Database.Models;

public class ImageTag
{
    public int ImageId { get; set; }
    public Image Image { get; set; }

    public int TagId { get; set; }
    public Tag Tag { get; set; }
}
