using System.Text.Json.Serialization;

namespace ReenPaystack.Models;

public class ChargeAuthorizationRequest
{
    [JsonPropertyName("authorization_code")]
    public string AuthorizationCode { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = "NGN";

    [JsonPropertyName("reference")]
    public string? Reference { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }

    [JsonPropertyName("split_code")]
    public string? SplitCode { get; set; }

    [JsonPropertyName("subaccount")]
    public string? Subaccount { get; set; }

    [JsonPropertyName("transaction_charge")]
    public decimal? TransactionCharge { get; set; }

    [JsonPropertyName("bearer")]
    public string? Bearer { get; set; }

    [JsonPropertyName("queue")]
    public bool Queue { get; set; } = false;
}

public class ChargeResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("domain")]
    public string Domain { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("reference")]
    public string Reference { get; set; } = string.Empty;

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("gateway_response")]
    public string GatewayResponse { get; set; } = string.Empty;

    [JsonPropertyName("paid_at")]
    public DateTime? PaidAt { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("channel")]
    public string Channel { get; set; } = string.Empty;

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    [JsonPropertyName("ip_address")]
    public string? IpAddress { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }

    [JsonPropertyName("fees")]
    public decimal? Fees { get; set; }

    [JsonPropertyName("customer")]
    public CustomerInfo? Customer { get; set; }

    [JsonPropertyName("authorization")]
    public AuthorizationInfo? Authorization { get; set; }
}