using System.ComponentModel.DataAnnotations;
public class Participant
{
    [Key]
    public int ParticipantID { get; set; }
    public int UserID { get; set; }
    public int PostID { get; set; }
}
