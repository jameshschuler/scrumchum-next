using System.ComponentModel.DataAnnotations;
using Scrumchum.Models.Enums;

namespace Scrumchum.Models.Request;

public class JoinRoomRequest
{
    [Required] 
    public string RoomCode { get; set; } = null!;

    [Required] 
    public string Username { get; set; } = null!;
    
    [Required]
    public UserRole Role { get; set; } = UserRole.Spectator;
}