using System.ComponentModel.DataAnnotations;
using Scrumchum.Models.Enums;

namespace Scrumchum.Models.Request;

public class CreateRoomRequest
{
    [Required] 
    public string Username { get; set; } = null!;
    
    public string? RoomName { get; set; }

    [Required]
    public UserRole Role { get; set; } = UserRole.Spectator;
}