using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Favorite
{
    [Key]
    public int FavoriteID { get; set; }
    public int UserID { get; set; }
    public int PostID { get; set; }
}
