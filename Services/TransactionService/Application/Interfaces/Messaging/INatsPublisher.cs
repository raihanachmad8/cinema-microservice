﻿namespace TransactionService.Application.Interfaces.Messaging
{
    public interface INatsPublisher
    {
        Task PublishAsync<T>(string subject, T message);
    }
}