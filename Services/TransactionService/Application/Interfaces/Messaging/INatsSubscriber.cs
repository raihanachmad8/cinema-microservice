namespace TransactionService.Application.Interfaces.Messaging
{
    public interface INatsSubscriber
    {
        void Subscribe<T>(string subject, Action<T> handler);
        void SubscribeAsync<TRequest, TResponse>(string subject, Func<TRequest, Task<TResponse>> handler);
    }
}