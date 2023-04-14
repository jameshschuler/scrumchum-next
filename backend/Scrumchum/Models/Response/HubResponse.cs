namespace Scrumchum.Models.Response;

public record HubResponse<T> : BaseResponse
{
    public T Data { get; set; }
}