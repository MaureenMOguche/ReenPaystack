using ReenPaystack.Models;

namespace ReenPaystack.Interfaces;

public interface IPaystackWebhookService
{
    bool VerifySignature(string payload, string signature, string secret);
    PaystackWebhookEvent<T> ParseWebhookEvent<T>(string payload) where T : class;
    PaystackWebhookEvent ParseWebhookEvent(string payload);
    bool IsValidEvent(string eventType);
    Task<bool> ProcessWebhookAsync<T>(string payload, string signature, string secret, Func<PaystackWebhookEvent<T>, Task> handler) where T : class;
    Task<bool> ProcessWebhookAsync(string payload, string signature, string secret, Func<PaystackWebhookEvent, Task> handler);
}

public interface IPaystackWebhookHandler<T> where T : class
{
    Task HandleAsync(PaystackWebhookEvent<T> webhookEvent);
    string EventType { get; }
}

public interface IPaystackWebhookHandler
{
    Task HandleAsync(PaystackWebhookEvent webhookEvent);
    string EventType { get; }
}