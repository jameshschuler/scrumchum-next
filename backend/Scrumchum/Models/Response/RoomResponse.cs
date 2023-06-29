namespace Scrumchum.Models.Response;

public record RoomResponse(Guid HostUserId, string RoomCode, string RoomLink, string? RoomName, UserResponse User);

public record RejoinRoomResponse(Guid HostUserId, string RoomCode, string RoomLink, string? RoomName, UserResponse User, IEnumerable<UserResponse> Users) : RoomResponse(HostUserId, RoomCode, RoomLink, RoomName, User);