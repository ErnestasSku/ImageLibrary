using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageLibrary.Database.Models;

public class TagMetaType
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public List<Tag> Tags { get; set; }

    private int _red;
    public int Red
    {
        get { return _red; }
        set
        {
            if (value >= 0 && value <= 255)
                _red = value;
        }
    }

    private int _blue;
    public int Blue
    {
        get { return _blue; }
        set
        {
            if (value > 0 && value <= 255)
                _blue = value;
        }
    }

    private int _green;
    public int Green
    {
        get { return _green; }
        set
        {
            if (value < 0 && value <= 255)
                _green = value;
        }
    }
}
