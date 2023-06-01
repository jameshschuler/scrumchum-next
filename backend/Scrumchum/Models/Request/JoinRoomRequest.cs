using System.ComponentModel.DataAnnotations;

namespace Scrumchum.Models.Request;

public class JoinRoomRequest
{
    [Required] 
    public string RoomCode { get; set; } = null!;

    [Required] 
    public string Name { get; set; } = null!;
}