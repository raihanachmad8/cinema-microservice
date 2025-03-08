using System.Text.Json.Serialization;

namespace TransactionService.Application.DTOs.Responses;

public record Response<T>
{
    public int Status { get; set; } = 200;
    public string Title { get; set; } = string.Empty;
    public string Detail { get; set; } = string.Empty;
    public T? Data { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Metadata? Metadata { get; set; }

    public Response<T> Ok(T data, string message = "Ok", Metadata? metadata = null)
    {
        return new Response<T>
        {
            Status = 200,
            Title = "Ok",
            Detail = message,
            Data = data,
            Metadata = metadata
        };
    }

    public Response<T> Created(T data, string message = "Created")
    {
        return new Response<T>
        {
            Status = 201,
            Title = "Created",
            Detail = message,
            Data = data
        };
    }
}