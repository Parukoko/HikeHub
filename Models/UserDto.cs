namespace HikeHub.Models
{
    public class UserDto
    {
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? Sex { get; set; }
        public int? Tel { get; set; }
        public string? IdLine { get; set; }
        public string? Bio { get; set; }
        public string? Image { get; set; }
        public List<FollowInfo> FollowedList { get; set; } = new List<FollowInfo>();
        public List<FollowInfo> FollowerList { get; set; } = new List<FollowInfo>();
    }
}