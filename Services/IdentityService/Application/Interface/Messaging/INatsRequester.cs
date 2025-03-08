namespace IdentityService.Application.Interfaces.Messaging
{
    public interface INatsRequester
    {
        Task<TResponse> Request<TRequest, TResponse>(string subject, TRequest request);
    }
}