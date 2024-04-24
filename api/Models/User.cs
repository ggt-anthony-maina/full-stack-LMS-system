using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

public class User 
{[Key]
    public string? Id { get; set; }
    [Required]
    public string? Username { get; set; }
    [Required]
    public required string Password { get; set; }
    [Required]
    public string? Email { get; set; }
    // Add more properties as needed
}
