using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HikeHub.Models
{
	public class Announcement
	{
		[Key]
		public int AnnouncementID { get; set; }
		[ForeignKey("User")]
		public int UserID { get; set; }
		public int PostID { get; set; }
		public virtual Post? Post { get; set; }
		public List<Participant>? Participants { get; set; }
		public virtual User? User { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? Image { get; set; }
        public DateTime UpdatedAt { get; set; }
	}
}
