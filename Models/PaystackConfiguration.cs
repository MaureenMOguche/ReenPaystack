namespace ReenPaystack.Models;

public class PaystackConfiguration
{
    public const string SectionName = "Paystack";
    
    public string SecretKey { get; set; } = string.Empty;
    public string PublicKey { get; set; } = string.Empty;
    public bool IsLiveMode { get; set; } = false;
    public string WebhookSecret { get; set; } = string.Empty;
    public int TimeoutInSeconds { get; set; } = 30;
}