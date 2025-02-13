using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HikeHub.Models
{
    public class User
    {

        [Key]
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set;}
        public int? Tel { get; set; }
        public string? IdLine { get; set; }
        public string? Bio { get; set; }
        public string? Image { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
        public virtual ICollection<Favourite>? Favourites { get; set; }
        public virtual ICollection<Participant>? Participants { get; set; }

    }
}
