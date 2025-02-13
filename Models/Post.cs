using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HikeHub.Models
{
    public class Post
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public int AnnouncementID { get; set; }
        public required string Title { get; set; }
        public string? Image { get; set; }
        public string? MeetingLocation { get; set; }
        public string? MeetingProvince { get; set; }
        public string? DestinationAddress { get; set; }
        public int? Duration { get; set; }
        public string? Description { get; set; }
        public int? MaxParticipants { get; set; }
        public DateTime DepartedDate { get; set; }
        public DateTime ReturnedDate  { get; set; }
        public DateTime ExpiredDate { get; set; }
        public virtual User? User { get; set; }
        public virtual List<Tag> TagList { get; set; } = new List<Tag>();
        public virtual List<Participant> Participants { get; set; } = new List<Participant>();
        public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();
        public virtual ICollection<Announcement>? Announcements { get; set; }
        public enum Status
        {
            Open,
            Closed
        }

        public Status PostStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
