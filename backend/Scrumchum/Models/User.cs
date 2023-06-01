using Scrumchum.Models.Enums;

namespace Scrumchum.Models;

public class User
{
    public string? ConnectionId { get; set; }
    
    public string Name { get; set; } = null!;
    
    public UserRole Role { get; set; }
}