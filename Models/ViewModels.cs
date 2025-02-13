using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HikeHub.Models;

namespace HikeHub.ViewModels
{
    public class TableViewModel
    {
        public List<string> TableNames { get; set; } = new List<string>();
    }

    public class PostViewModel
    {
        public int PostID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(255, ErrorMessage = "Title cannot be longer than 255 characters.")]
        public required string Title { get; set; }

        public string? Image { get; set; }

        [Required(ErrorMessage = "Meeting Location is required")]
        public required string MeetingLocation { get; set; }
        [Required(ErrorMessage = "Meeting Province is required")]
        public required string MeetingProvince { get; set; }

        [Required(ErrorMessage = "Destination Address is required")]
        public required string DestinationAddress { get; set; }
        public int Duration => (ReturnedDate - DepartedDate).Days;

        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Max Participants must be at least 1")]
        public int MaxParticipants { get; set; }

        [Required(ErrorMessage = "Departed Date is required")]
        public DateTime DepartedDate { get; set; }

        [Required(ErrorMessage = "Returned Date is required")]
        public DateTime ReturnedDate { get; set; }

        [Required(ErrorMessage = "Expiration date must be in the future.")]
        public DateTime ExpiredDate { get; set; }
        public UserViewModel? User { get; set; }
        public List<TagViewModel> Tags { get; set; } = new List<TagViewModel>();
        public List<ParticipantViewModel> Participants { get; set; } = new List<ParticipantViewModel>();
        public Post.Status PostStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Announcement? Announcement { get; set; }
    }

    public class UserViewModel
    {
        public int UserID { get; set; }
        public required string UserName { get; set; }
    }

    public class TagViewModel
    {
        public int TagID { get; set; }
        public required string TagName { get; set; }
    }

    public class ParticipantViewModel
    {
        public int ParticipantID { get; set; }
        public int UserID { get; set; }
        public int PostID { get; set; }
    }

    public class CreatePostViewModel
    {
        [Required]
        public required string Title { get; set; }

        public string? Image { get; set; }

        [Required]
        public required string MeetingLocation { get; set; }

        [Required]
        public required string MeetingProvince { get; set; }

        [Required]
        public required string DestinationAddress { get; set; }

        [Required]
        public required int Duration { get; set; }

        public string? Description { get; set; }

        [Range(1, int.MaxValue)]
        public int MaxParticipants { get; set; }

        public DateTime ReturnedDate { get; set; }

        [Required(ErrorMessage = "Departed Date is required")]
        public DateTime DepartedDate { get; set; }

        [Required(ErrorMessage = "Expiration Date is required")]
        public DateTime ExpiredDate { get; set; }
    }

    public class PostListViewModel
    {
        public List<PostViewModel> Posts { get; set; } = new List<PostViewModel>();
    }

    public class ChangeStatusViewModel
    {
        public int PostId { get; set; }
        public Post.Status PostStatus { get; set; } = Post.Status.Closed;
    }
}
