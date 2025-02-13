using System.ComponentModel.DataAnnotations;
namespace HikeHub.Models
{
    public class Participant
    {
        [Key]
        public int ParticipantID { get; set; }
        public int AnnouncementID { get; set; }
        public int UserID { get; set; }
        public int PostID { get; set; }
        public virtual User? User { get; set; }
        public virtual Announcement? Announcement { get; set; }
    }
}
