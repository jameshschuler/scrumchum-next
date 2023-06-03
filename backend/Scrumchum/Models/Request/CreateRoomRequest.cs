using FluentValidation;
using Scrumchum.Models.Enums;

namespace Scrumchum.Models.Request;

public class CreateRoomRequest
{
    public string? Username { get; set; }
    
    public string? RoomName { get; set; }

    public UserRole? Role { get; set; } = UserRole.Spectator;
}

public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequest>
{
    public CreateRoomRequestValidator()
    {
        RuleFor(r => r.Username)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(20);

        RuleFor(r => r.RoomName)
            .MinimumLength(1)
            .MaximumLength(50);

        RuleFor(r => r.Role)
            .IsInEnum();
    }
}