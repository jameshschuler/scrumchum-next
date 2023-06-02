namespace Scrumchum.Models.Response;

public record BaseResponse
{
    public string? ErrorMessage { get; set; }

    public bool Success
    {
        get
        {
            return string.IsNullOrWhiteSpace(ErrorMessage);
        }
    }
}