using Scrumchum.Models.Enums;

namespace Scrumchum.Models.Response;

public record UserResponse(bool IsHost, string Username, UserRole Role, Guid? UserId = null);