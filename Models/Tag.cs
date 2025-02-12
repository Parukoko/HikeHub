using System.ComponentModel.DataAnnotations;
public class Tag
{
    [Key]
    public int TagID { get; set; }
    public TagType TagName { get; set; }
    public enum TagType
    {
        Northern,
        Northeastern,
        Western,
        Central,
        Eastern,
        Southern
    }
}
