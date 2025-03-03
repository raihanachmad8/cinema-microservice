namespace IdentityService.Application.Dtos.Responses
{
    public interface IResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }   
    }
}