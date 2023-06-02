namespace Scrumchum.Models.Response;

public record RoomResponse(string RoomCode, string RoomLink, string? RoomName, UserResponse User);