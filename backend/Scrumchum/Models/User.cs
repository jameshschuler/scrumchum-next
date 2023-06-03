using Scrumchum.Models.Enums;

namespace Scrumchum.Models;

public class User
{
    public string? ConnectionId { get; set; }
    
    public int UserId { get; set; }
    
    public string Username { get; set; } = null!;
    
    public UserRole Role { get; set; }
}