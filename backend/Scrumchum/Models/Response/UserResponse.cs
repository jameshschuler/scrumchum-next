using Scrumchum.Models.Enums;

namespace Scrumchum.Models.Response;

public record UserResponse(string Username, UserRole Role, int? UserId = null);