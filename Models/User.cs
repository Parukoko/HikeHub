using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int UserID { get; set; }
    
    [Required]
    public string UserName { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime Birthdate { get; set; }
    
    public string Bio { get; set; } = string.Empty;
    
    [Required]
    [RegularExpression("Male|Female", ErrorMessage = "Sex must be either 'Male' or 'Female'")]
    public string Sex { get; set; } = string.Empty;
    
    public string ProfilePicture { get; set; } = string.Empty;

    public List<int> FollowList { get; set; } = new List<int>();

    public List<int> FollowerList { get; set; } = new List<int>();

    public List<int> PostList { get; set; } = new List<int>();
}