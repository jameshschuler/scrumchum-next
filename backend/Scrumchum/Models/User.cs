using Scrumchum.Models.Enums;
using Scrumchum.Models.Response;

namespace Scrumchum.Models;

public class User
{
    public string? ConnectionId { get; set; }
    
    public Guid UserId { get; set; }
    
    public string Username { get; set; } = null!;
    
    public bool IsHost { get; set; }
    
    public UserRole Role { get; set; }

    public UserResponse ToUserResponse() => new UserResponse(IsHost, Username, Role, UserId);
}