namespace Scrumchum.Models;

public class Room
{
    public string RoomCode { get; set; } = null!;
    
    public string? RoomName { get; set; }

    public IList<User> Users { get; set; } = new List<User>();
    
    public Guid HostUserId { get; set; }
}