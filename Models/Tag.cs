using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Tag
{
    [Key]
    public int TagID { get; set; }
    public string TagName { get; set; }
}
