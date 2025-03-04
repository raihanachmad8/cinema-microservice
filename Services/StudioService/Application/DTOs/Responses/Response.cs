using System.Text.Json.Serialization;

namespace StudioService.Application.DTOs.Responses;

public record Response<T>
{
    public string Status { get; set; } = "200";
    public string Message { get; set; } = "OK";
    public T? Data { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Metadata? Metadata { get; set; }

    public Response<T> Ok(T data, string message = "Ok", Metadata? metadata = null)
    {
        return new Response<T>
        {
            Status = "200",
            Message = message,
            Data = data,
            Metadata = metadata
        };
    }

    public Response<T> Created(T data, string message = "Created")
    {
        return new Response<T>
        {
            Status = "201",
            Message = message,
            Data = data
        };
    }
}