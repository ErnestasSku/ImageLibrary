using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageLibrary.Database.Models;

public class TagAssociation
{
    [Key]
    public string AssosiactionId { get; set; }
    public int TagAId { get; set; }
    public Tag TagA { get; set; }
    public int TagBId { get; set; }
    public Tag TagB { get; set; }

    public TagAssociation(string assosiactionId, int tagAId, Tag tagA, int tagBId, Tag tagB)
    {
        AssosiactionId = assosiactionId;
        TagAId = tagAId;
        TagA = tagA;
        TagBId = tagBId;
        TagB = tagB;
    }

    public TagAssociation(int tagAId, int tagBId)
    {
        TagAId = tagAId;
        TagBId = tagBId;
    }
}
