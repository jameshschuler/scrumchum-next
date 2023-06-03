namespace Scrumchum.Models.Response;

public record BaseResponse
{
    public string? ErrorMessage { get; set; }

    public IEnumerable<ErrorResponse> ValidationErrors { get; set; } = new List<ErrorResponse>();

    public bool Success => string.IsNullOrWhiteSpace(ErrorMessage) || ValidationErrors.Any();
}