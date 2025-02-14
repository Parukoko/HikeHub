using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HikeHub.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [Required]
        [RegularExpression("Male|Female", ErrorMessage = "Sex must be either 'Male' or 'Female'")]
        public string Sex { get; set; } = string.Empty;

        public int? Tel { get; set; }
        public string? IdLine { get; set; }
        public string? Bio { get; set; }
        public string? Image { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
        public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();
        public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
        public object? ProfilePicture { get; set; }

        public List<int> FollowList { get; set; } = new List<int>();
        public List<int> FollowerList { get; set; } = new List<int>();
    }
}