namespace Scrumchum.Models.Response;

public record UserJoinedResponse(string RoomCode, IEnumerable<UserResponse> Users);