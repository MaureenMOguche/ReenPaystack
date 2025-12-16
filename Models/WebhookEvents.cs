using System.Text.Json.Serialization;

namespace ReenPaystack.Models;

public class PaystackWebhookEvent
{
    [JsonPropertyName("event")]
    public string Event { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public object Data { get; set; } = new();
}

public class PaystackWebhookEvent<T> where T : class
{
    [JsonPropertyName("event")]
    public string Event { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public T Data { get; set; } = default!;
}

public class ChargeSuccessEvent
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
    public WebhookCustomerInfo? Customer { get; set; }

    [JsonPropertyName("authorization")]
    public WebhookAuthorizationInfo? Authorization { get; set; }

    [JsonPropertyName("plan")]
    public WebhookPlanInfo? Plan { get; set; }
}

public class CustomerIdentificationEvent
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("customer_id")]
    public int CustomerId { get; set; }

    [JsonPropertyName("customer_code")]
    public string CustomerCode { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    [JsonPropertyName("validated")]
    public bool Validated { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
}

public class DedicatedAccountAssignSuccessEvent
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

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("assigned")]
    public bool Assigned { get; set; }

    [JsonPropertyName("assignment")]
    public DedicatedAccountAssignment? Assignment { get; set; }

    [JsonPropertyName("bank")]
    public DedicatedAccountBank? Bank { get; set; }

    [JsonPropertyName("customer")]
    public WebhookCustomerInfo? Customer { get; set; }
}

public class TransferSuccessEvent
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("reference")]
    public string Reference { get; set; } = string.Empty;

    [JsonPropertyName("source")]
    public string Source { get; set; } = string.Empty;

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("transfer_code")]
    public string TransferCode { get; set; } = string.Empty;

    [JsonPropertyName("reason")]
    public string? Reason { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("recipient")]
    public WebhookTransferRecipientInfo? Recipient { get; set; }
}

public class InvoiceSuccessEvent
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("domain")]
    public string Domain { get; set; } = string.Empty;

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    [JsonPropertyName("due_date")]
    public DateTime? DueDate { get; set; }

    [JsonPropertyName("has_invoice")]
    public bool HasInvoice { get; set; }

    [JsonPropertyName("invoice_number")]
    public int InvoiceNumber { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("pdf_url")]
    public string? PdfUrl { get; set; }

    [JsonPropertyName("line_items")]
    public WebhookLineItem[]? LineItems { get; set; }

    [JsonPropertyName("tax")]
    public WebhookTaxInfo[]? Tax { get; set; }

    [JsonPropertyName("request_code")]
    public string RequestCode { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("paid")]
    public bool Paid { get; set; }

    [JsonPropertyName("paid_at")]
    public DateTime? PaidAt { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("customer")]
    public WebhookCustomerInfo? Customer { get; set; }
}

public class SubscriptionSuccessEvent
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("domain")]
    public string Domain { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("subscription_code")]
    public string SubscriptionCode { get; set; } = string.Empty;

    [JsonPropertyName("email_token")]
    public string EmailToken { get; set; } = string.Empty;

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("cron_expression")]
    public string CronExpression { get; set; } = string.Empty;

    [JsonPropertyName("next_payment_date")]
    public DateTime? NextPaymentDate { get; set; }

    [JsonPropertyName("open_invoice")]
    public string? OpenInvoice { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("plan")]
    public WebhookPlanInfo? Plan { get; set; }

    [JsonPropertyName("authorization")]
    public WebhookAuthorizationInfo? Authorization { get; set; }

    [JsonPropertyName("customer")]
    public WebhookCustomerInfo? Customer { get; set; }
}

public class WebhookCustomerInfo
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

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }

    [JsonPropertyName("risk_action")]
    public string? RiskAction { get; set; }

    [JsonPropertyName("international_format_phone")]
    public string? InternationalFormatPhone { get; set; }
}

public class WebhookAuthorizationInfo
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

public class WebhookPlanInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("plan_code")]
    public string PlanCode { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("interval")]
    public string Interval { get; set; } = string.Empty;

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;
}

public class WebhookTransferRecipientInfo
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

public class WebhookLineItem
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}

public class WebhookTaxInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
}

public static class PaystackWebhookEvents
{
    public const string ChargeSuccess = "charge.success";
    public const string ChargeFailed = "charge.failed";
    public const string TransferSuccess = "transfer.success";
    public const string TransferFailed = "transfer.failed";
    public const string TransferReversed = "transfer.reversed";
    public const string DedicatedAccountAssignSuccess = "dedicatedaccount.assign.success";
    public const string DedicatedAccountAssignFailed = "dedicatedaccount.assign.failed";
    public const string CustomerIdentificationSuccess = "customeridentification.success";
    public const string CustomerIdentificationFailed = "customeridentification.failed";
    public const string InvoiceCreate = "invoice.create";
    public const string InvoicePaymentSuccess = "invoice.payment_success";
    public const string InvoicePaymentFailed = "invoice.payment_failed";
    public const string SubscriptionCreate = "subscription.create";
    public const string SubscriptionDisable = "subscription.disable";
    public const string SubscriptionNotRenew = "subscription.not_renew";
    public const string PaymentRequestSuccess = "paymentrequest.success";
    public const string PaymentRequestPending = "paymentrequest.pending";
}