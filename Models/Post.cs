using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HikeHub.Models
{
    public class Post
    {
        [Key]
        public int PostID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        public Status PostStatus { get; set; } = Status.Open;

		 public enum Status
        {
            Open,
            Closed
        }


        public virtual User User { get; set; }

        public required string Title { get; set; }

        public string? Image { get; set; }

        public required string MeetingLocation { get; set; }

        public required string DestinationAddress { get; set; }

        public required int Duration { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<Tag> TagList { get; set; } = new List<Tag>();

        [Range(1, int.MaxValue)]
        public int MaxParticipants { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        [Required]
        [FutureDate(ErrorMessage = "Expiration date must be in the future.")]
        public DateTime ExpiredAt { get; set; }

        public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>(); // ✅ Added Participants List

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
