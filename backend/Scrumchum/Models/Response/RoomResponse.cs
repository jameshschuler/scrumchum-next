namespace Scrumchum.Models.Response;

public record RoomResponse(Guid HostUserId, string RoomCode, string RoomLink, string? RoomName, UserResponse User);