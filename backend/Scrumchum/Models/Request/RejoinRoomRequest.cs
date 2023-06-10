using FluentValidation;

namespace Scrumchum.Models.Request;

public class RejoinRoomRequest
{
    public string? UserId { get; set; }
    
    public string? RoomCode { get; set; }
}

public class RejoinRoomRequestValidator : AbstractValidator<RejoinRoomRequest>
{
    public RejoinRoomRequestValidator()
    {
        RuleFor(r => r.RoomCode)
            .NotEmpty();

        RuleFor(r => r.UserId)
            .NotEmpty();
    }
}