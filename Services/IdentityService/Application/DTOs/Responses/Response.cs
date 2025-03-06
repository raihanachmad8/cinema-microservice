using System.Text.Json.Serialization;

namespace MovieService.Application.DTOs.Responses;

public record Response<T>
{
    public string Status { get; set; } = "200";
    public string Message { get; set; } = "OK";
    public T? Data { get; set; }


    public Response<T> Ok(T data, string message = "Ok")
    {
        return new Response<T>
        {
            Status = "200",
            Message = message,
            Data = data,
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