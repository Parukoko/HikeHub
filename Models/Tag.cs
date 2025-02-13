using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HikeHub.Models
{
    public class Tag
    {
        [Key]
        public int TagID { get; set; }
        public TagType TagName { get; set; }
        [ForeignKey("Post")]
        public int PostID { get; set; }
        public Post? Post { get; set; }
        public enum TagType
        {
            Northern = 0,
            Northeastern = 1,
            Western = 2,
            Central = 3,
            Eastern = 4,
            Southern = 5
        }
        public string GetTagNameString()
        {
            return TagName.ToString();
        }
    }
}
