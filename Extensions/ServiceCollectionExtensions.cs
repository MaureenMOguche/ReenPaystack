using Microsoft.Extensions.DependencyInjection;
using ReenPaystack.Interfaces;
using ReenPaystack.Models;
using ReenPaystack.Services;
using ReenPaystack.Services.WebhookHandlers;

namespace ReenPaystack.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPaystackClient(this IServiceCollection services, string secretKey)
    {
        services.AddHttpClient<PaystackClient>();
        services.AddTransient<IPaystackClient>(provider =>
        {
            var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient(nameof(PaystackClient));
            return new PaystackClient(httpClient, secretKey);
        });

        return services;
    }

    public static IServiceCollection AddPaystackClient(this IServiceCollection services, Action<PaystackOptions> configure)
    {
        var options = new PaystackOptions();
        configure(options);

        return services.AddPaystackClient(options.SecretKey);
    }

    public static IServiceCollection AddPaystackWebhooks(this IServiceCollection services)
    {
        services.AddTransient<IPaystackWebhookService, PaystackWebhookService>();
        services.AddTransient<IPaystackWebhookDispatcher, PaystackWebhookDispatcher>();
        
        return services;
    }

    public static IServiceCollection AddPaystackWebhookHandlers(this IServiceCollection services)
    {
        // Add default webhook handlers
        services.AddTransient<IPaystackWebhookHandler<ChargeSuccessEvent>, ChargeSuccessHandler>();
        services.AddTransient<IPaystackWebhookHandler<TransferSuccessEvent>, TransferSuccessHandler>();
        services.AddTransient<IPaystackWebhookHandler<DedicatedAccountAssignSuccessEvent>, DedicatedAccountAssignSuccessHandler>();
        services.AddTransient<IPaystackWebhookHandler, GenericWebhookHandler>();

        return services;
    }

    public static IServiceCollection AddPaystackWebhookHandler<TEvent, THandler>(this IServiceCollection services)
        where TEvent : class
        where THandler : class, IPaystackWebhookHandler<TEvent>
    {
        services.AddTransient<IPaystackWebhookHandler<TEvent>, THandler>();
        return services;
    }

    public static IServiceCollection AddPaystackWebhookHandler<THandler>(this IServiceCollection services)
        where THandler : class, IPaystackWebhookHandler
    {
        services.AddTransient<IPaystackWebhookHandler, THandler>();
        return services;
    }
}

public class PaystackOptions
{
    public string SecretKey { get; set; } = string.Empty;
}