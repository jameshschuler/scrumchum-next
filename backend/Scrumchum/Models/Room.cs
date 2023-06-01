namespace Scrumchum.Models;

public class Room
{
    public string RoomCode { get; set; } = null!;

    public IList<User> Users { get; set; } = new List<User>();
}