using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HikeHub.Models;

public class Favourite
{
    [Key]
    public int FavouriteID { get; set; }
    public int UserID { get; set; }
    public int PostID { get; set; }
    public DateTime LikedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("UserId")]
    public virtual User? User { get; set; }

    [ForeignKey("PostId")]
    public virtual Post? Post { get; set; }
}
