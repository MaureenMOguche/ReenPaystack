using System.Text.Json.Serialization;

namespace ReenPaystack.Models;

public class CreateDedicatedAccountRequest
{
    [JsonPropertyName("customer")]
    public string Customer { get; set; } = string.Empty;

    [JsonPropertyName("preferred_bank")]
    public string? PreferredBank { get; set; }

    [JsonPropertyName("subaccount")]
    public string? Subaccount { get; set; }

    [JsonPropertyName("split_code")]
    public string? SplitCode { get; set; }

    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }
}

public class DedicatedAccountResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("account_name")]
    public string AccountName { get; set; } = string.Empty;

    [JsonPropertyName("account_number")]
    public string AccountNumber { get; set; } = string.Empty;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    [JsonPropertyName("split_code")]
    public string? SplitCode { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("assigned")]
    public bool Assigned { get; set; }

    [JsonPropertyName("assignment")]
    public DedicatedAccountAssignment? Assignment { get; set; }

    [JsonPropertyName("bank")]
    public DedicatedAccountBank? Bank { get; set; }

    [JsonPropertyName("customer")]
    public DedicatedAccountCustomer? Customer { get; set; }
}

public class DedicatedAccountAssignment
{
    [JsonPropertyName("integration")]
    public int Integration { get; set; }

    [JsonPropertyName("assignee_id")]
    public int AssigneeId { get; set; }

    [JsonPropertyName("assignee_type")]
    public string AssigneeType { get; set; } = string.Empty;

    [JsonPropertyName("expired")]
    public bool Expired { get; set; }

    [JsonPropertyName("account_type")]
    public string AccountType { get; set; } = string.Empty;

    [JsonPropertyName("assigned_at")]
    public DateTime AssignedAt { get; set; }
}

public class DedicatedAccountBank
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;
}

public class DedicatedAccountCustomer
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

    [JsonPropertyName("risk_action")]
    public string? RiskAction { get; set; }
}

public class ListDedicatedAccountsResponse
{
    [JsonPropertyName("accounts")]
    public DedicatedAccountResponse[] Accounts { get; set; } = [];

    [JsonPropertyName("meta")]
    public PaginationMeta? Meta { get; set; }
}

public class PaginationMeta
{
    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("skipped")]
    public int Skipped { get; set; }

    [JsonPropertyName("perPage")]
    public int PerPage { get; set; }

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("pageCount")]
    public int PageCount { get; set; }
}

public class DeactivateDedicatedAccountRequest
{
    [JsonPropertyName("account_number")]
    public string AccountNumber { get; set; } = string.Empty;
}

public class SplitDedicatedAccountRequest
{
    [JsonPropertyName("customer")]
    public string Customer { get; set; } = string.Empty;

    [JsonPropertyName("subaccount")]
    public string? Subaccount { get; set; }

    [JsonPropertyName("split_code")]
    public string? SplitCode { get; set; }

    [JsonPropertyName("preferred_bank")]
    public string? PreferredBank { get; set; }
}