using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using ReenPaystack.Interfaces;
using ReenPaystack.Models;

namespace ReenPaystack.Services;

public class PaystackWebhookService : IPaystackWebhookService
{
    private readonly JsonSerializerOptions _jsonOptions;

    public PaystackWebhookService()
    {
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            PropertyNameCaseInsensitive = true
        };
    }

    public bool VerifySignature(string payload, string signature, string secret)
    {
        if (string.IsNullOrEmpty(payload) || string.IsNullOrEmpty(signature) || string.IsNullOrEmpty(secret))
        {
            return false;
        }

        try
        {
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var payloadBytes = Encoding.UTF8.GetBytes(payload);

            using var hmac = new HMACSHA512(secretBytes);
            var computedHashBytes = hmac.ComputeHash(payloadBytes);
            var computedHash = Convert.ToHexString(computedHashBytes).ToLowerInvariant();

            return computedHash.Equals(signature, StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }

    public PaystackWebhookEvent<T> ParseWebhookEvent<T>(string payload) where T : class
    {
        if (string.IsNullOrEmpty(payload))
        {
            throw new ArgumentException("Payload cannot be null or empty", nameof(payload));
        }

        try
        {
            var webhookEvent = JsonSerializer.Deserialize<PaystackWebhookEvent<T>>(payload, _jsonOptions);
            return webhookEvent ?? throw new InvalidOperationException("Failed to deserialize webhook event");
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException("Invalid JSON payload", ex);
        }
    }

    public PaystackWebhookEvent ParseWebhookEvent(string payload)
    {
        if (string.IsNullOrEmpty(payload))
        {
            throw new ArgumentException("Payload cannot be null or empty", nameof(payload));
        }

        try
        {
            var webhookEvent = JsonSerializer.Deserialize<PaystackWebhookEvent>(payload, _jsonOptions);
            return webhookEvent ?? throw new InvalidOperationException("Failed to deserialize webhook event");
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException("Invalid JSON payload", ex);
        }
    }

    public bool IsValidEvent(string eventType)
    {
        if (string.IsNullOrEmpty(eventType))
        {
            return false;
        }

        var validEvents = new[]
        {
            PaystackWebhookEvents.ChargeSuccess,
            PaystackWebhookEvents.ChargeFailed,
            PaystackWebhookEvents.TransferSuccess,
            PaystackWebhookEvents.TransferFailed,
            PaystackWebhookEvents.TransferReversed,
            PaystackWebhookEvents.DedicatedAccountAssignSuccess,
            PaystackWebhookEvents.DedicatedAccountAssignFailed,
            PaystackWebhookEvents.CustomerIdentificationSuccess,
            PaystackWebhookEvents.CustomerIdentificationFailed,
            PaystackWebhookEvents.InvoiceCreate,
            PaystackWebhookEvents.InvoicePaymentSuccess,
            PaystackWebhookEvents.InvoicePaymentFailed,
            PaystackWebhookEvents.SubscriptionCreate,
            PaystackWebhookEvents.SubscriptionDisable,
            PaystackWebhookEvents.SubscriptionNotRenew,
            PaystackWebhookEvents.PaymentRequestSuccess,
            PaystackWebhookEvents.PaymentRequestPending
        };

        return validEvents.Contains(eventType);
    }

    public async Task<bool> ProcessWebhookAsync<T>(string payload, string signature, string secret, Func<PaystackWebhookEvent<T>, Task> handler) where T : class
    {
        try
        {
            if (!VerifySignature(payload, signature, secret))
            {
                return false;
            }

            var webhookEvent = ParseWebhookEvent<T>(payload);

            if (!IsValidEvent(webhookEvent.Event))
            {
                return false;
            }

            await handler(webhookEvent);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> ProcessWebhookAsync(string payload, string signature, string secret, Func<PaystackWebhookEvent, Task> handler)
    {
        try
        {
            if (!VerifySignature(payload, signature, secret))
            {
                return false;
            }

            var webhookEvent = ParseWebhookEvent(payload);

            if (!IsValidEvent(webhookEvent.Event))
            {
                return false;
            }

            await handler(webhookEvent);
            return true;
        }
        catch
        {
            return false;
        }
    }
}