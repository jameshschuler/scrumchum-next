using FluentValidation;

namespace Scrumchum.Models.Request;

public class LeaveRoomRequest
{
    public string? RoomCode { get; set; }
}

public class LeaveRoomRequestValidator : AbstractValidator<LeaveRoomRequest>
{
    public LeaveRoomRequestValidator()
    {
        RuleFor(r => r.RoomCode)
            .NotEmpty();
    }
}