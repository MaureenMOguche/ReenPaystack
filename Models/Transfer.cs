using System.Text.Json.Serialization;

namespace ReenPaystack.Models;

public class InitiateTransferRequest
{
    [JsonPropertyName("source")]
    public string Source { get; set; } = "balance";

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = "NGN";

    [JsonPropertyName("recipient")]
    public string Recipient { get; set; } = string.Empty;

    [JsonPropertyName("reason")]
    public string? Reason { get; set; }

    [JsonPropertyName("reference")]
    public string? Reference { get; set; }
}

public class TransferResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("reference")]
    public string Reference { get; set; } = string.Empty;

    [JsonPropertyName("source")]
    public string Source { get; set; } = string.Empty;

    [JsonPropertyName("source_details")]
    public object? SourceDetails { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("failures")]
    public object? Failures { get; set; }

    [JsonPropertyName("transfer_code")]
    public string TransferCode { get; set; } = string.Empty;

    [JsonPropertyName("reason")]
    public string? Reason { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("recipient")]
    public TransferRecipientInfo? Recipient { get; set; }
}

public class CreateTransferRecipientRequest
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("account_number")]
    public string AccountNumber { get; set; } = string.Empty;

    [JsonPropertyName("bank_code")]
    public string BankCode { get; set; } = string.Empty;

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = "NGN";

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }
}

public class TransferRecipientResponse
{
    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    [JsonPropertyName("domain")]
    public string Domain { get; set; } = string.Empty;

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("integration")]
    public int Integration { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("recipient_code")]
    public string RecipientCode { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("is_deleted")]
    public bool IsDeleted { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("details")]
    public RecipientDetails? Details { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }
}

public class TransferRecipientInfo
{
    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    [JsonPropertyName("domain")]
    public string Domain { get; set; } = string.Empty;

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("integration")]
    public int Integration { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("recipient_code")]
    public string RecipientCode { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("details")]
    public RecipientDetails? Details { get; set; }
}

public class RecipientDetails
{
    [JsonPropertyName("authorization_code")]
    public string? AuthorizationCode { get; set; }

    [JsonPropertyName("account_number")]
    public string? AccountNumber { get; set; }

    [JsonPropertyName("account_name")]
    public string? AccountName { get; set; }

    [JsonPropertyName("bank_code")]
    public string? BankCode { get; set; }

    [JsonPropertyName("bank_name")]
    public string? BankName { get; set; }
}