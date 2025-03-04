namespace IdentityService.Application.Dtos.Responses
{
    public interface IResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }   
    }
}