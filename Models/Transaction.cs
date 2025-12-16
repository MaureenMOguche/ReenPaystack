using System.Text.Json.Serialization;

namespace ReenPaystack.Models;

public class InitializeTransactionRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = "NGN";

    [JsonPropertyName("reference")]
    public string? Reference { get; set; }

    [JsonPropertyName("callback_url")]
    public string? CallbackUrl { get; set; }

    [JsonPropertyName("plan")]
    public string? Plan { get; set; }

    [JsonPropertyName("invoice_limit")]
    public int? InvoiceLimit { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }

    [JsonPropertyName("channels")]
    public string[]? Channels { get; set; }

    [JsonPropertyName("split_code")]
    public string? SplitCode { get; set; }

    [JsonPropertyName("subaccount")]
    public string? Subaccount { get; set; }

    [JsonPropertyName("transaction_charge")]
    public decimal? TransactionCharge { get; set; }

    [JsonPropertyName("bearer")]
    public string? Bearer { get; set; }
}

public class TransactionResponse
{
    [JsonPropertyName("authorization_url")]
    public string AuthorizationUrl { get; set; } = string.Empty;

    [JsonPropertyName("access_code")]
    public string AccessCode { get; set; } = string.Empty;

    [JsonPropertyName("reference")]
    public string Reference { get; set; } = string.Empty;

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("gateway_response")]
    public string GatewayResponse { get; set; } = string.Empty;

    [JsonPropertyName("paid_at")]
    public DateTime? PaidAt { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("channel")]
    public string Channel { get; set; } = string.Empty;

    [JsonPropertyName("fees")]
    public decimal? Fees { get; set; }

    [JsonPropertyName("customer")]
    public CustomerInfo? Customer { get; set; }

    [JsonPropertyName("authorization")]
    public AuthorizationInfo? Authorization { get; set; }
}

public class CustomerInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("customer_code")]
    public string CustomerCode { get; set; } = string.Empty;

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }
}

public class AuthorizationInfo
{
    [JsonPropertyName("authorization_code")]
    public string AuthorizationCode { get; set; } = string.Empty;

    [JsonPropertyName("bin")]
    public string Bin { get; set; } = string.Empty;

    [JsonPropertyName("last4")]
    public string Last4 { get; set; } = string.Empty;

    [JsonPropertyName("exp_month")]
    public string ExpMonth { get; set; } = string.Empty;

    [JsonPropertyName("exp_year")]
    public string ExpYear { get; set; } = string.Empty;

    [JsonPropertyName("channel")]
    public string Channel { get; set; } = string.Empty;

    [JsonPropertyName("card_type")]
    public string CardType { get; set; } = string.Empty;

    [JsonPropertyName("bank")]
    public string Bank { get; set; } = string.Empty;

    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; } = string.Empty;

    [JsonPropertyName("brand")]
    public string Brand { get; set; } = string.Empty;

    [JsonPropertyName("reusable")]
    public bool Reusable { get; set; }

    [JsonPropertyName("signature")]
    public string Signature { get; set; } = string.Empty;

    [JsonPropertyName("account_name")]
    public string? AccountName { get; set; }
}