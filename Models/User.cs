using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int UserID { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string UserName { get; set; } = string.Empty;
    
    [Range(0, 120)]
    public int Age { get; set; }
    
    public string Bio { get; set; } = string.Empty;
    
    public string Gender { get; set; } = string.Empty;
    
    public required byte[] ProfilePicture { get; set; }
}