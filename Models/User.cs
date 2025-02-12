using System.ComponentModel.DataAnnotations;
public class User
{
    [Key]
    public int UserID { get; set; }
    public string UserName { get; set; } = string.Empty;
}
