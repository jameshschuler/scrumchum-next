using System.ComponentModel.DataAnnotations;
using Scrumchum.Models.Enums;

namespace Scrumchum.Models.Request;

public class CreateRoomRequest
{
    [Required] 
    public string Name { get; set; } = null!;
    
    public UserRole Role { get; set; }
}