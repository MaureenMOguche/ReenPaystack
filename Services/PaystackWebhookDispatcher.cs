using Microsoft.Extensions.DependencyInjection;
using ReenPaystack.Interfaces;
using ReenPaystack.Models;

namespace ReenPaystack.Services;

public interface IPaystackWebhookDispatcher
{
    Task<bool> ProcessWebhookAsync(string payload, string signature, string secret);
    Task<bool> ProcessWebhookAsync<T>(string payload, string signature, string secret) where T : class;
}

public class PaystackWebhookDispatcher : IPaystackWebhookDispatcher
{
    private readonly IPaystackWebhookService _webhookService;
    private readonly IServiceProvider _serviceProvider;

    public PaystackWebhookDispatcher(IPaystackWebhookService webhookService, IServiceProvider serviceProvider)
    {
        _webhookService = webhookService;
        _serviceProvider = serviceProvider;
    }

    public async Task<bool> ProcessWebhookAsync(string payload, string signature, string secret)
    {
        try
        {
            if (!_webhookService.VerifySignature(payload, signature, secret))
            {
                return false;
            }

            var webhookEvent = _webhookService.ParseWebhookEvent(payload);

            if (!_webhookService.IsValidEvent(webhookEvent.Event))
            {
                return false;
            }

            // Try to find specific handlers for this event type
            var handlers = _serviceProvider.GetServices<IPaystackWebhookHandler>()
                .Where(h => h.EventType == webhookEvent.Event || h.EventType == "*")
                .ToList();

            if (handlers.Any())
            {
                var tasks = handlers.Select(handler => handler.HandleAsync(webhookEvent));
                await Task.WhenAll(tasks);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> ProcessWebhookAsync<T>(string payload, string signature, string secret) where T : class
    {
        try
        {
            if (!_webhookService.VerifySignature(payload, signature, secret))
            {
                return false;
            }

            var webhookEvent = _webhookService.ParseWebhookEvent<T>(payload);

            if (!_webhookService.IsValidEvent(webhookEvent.Event))
            {
                return false;
            }

            // Try to find specific typed handlers for this event type
            var handlers = _serviceProvider.GetServices<IPaystackWebhookHandler<T>>()
                .Where(h => h.EventType == webhookEvent.Event)
                .ToList();

            if (handlers.Any())
            {
                var tasks = handlers.Select(handler => handler.HandleAsync(webhookEvent));
                await Task.WhenAll(tasks);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
}