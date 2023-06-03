using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Scrumchum.Models.Enums;

namespace Scrumchum.Models.Request;

public class JoinRoomRequest
{
    public string? RoomCode { get; set; } = null!;

    public string? Username { get; set; } = null!;
    
    public UserRole? Role { get; set; } = UserRole.Spectator;
}

public class JoinRoomRequestValidator : AbstractValidator<JoinRoomRequest>
{
    public JoinRoomRequestValidator()
    {
        RuleFor(r => r.Username)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(20);

        RuleFor(r => r.RoomCode)
            .NotEmpty();

        RuleFor(r => r.Role)
            .IsInEnum();
    }
}