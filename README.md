# ReenPaystack SDK

A comprehensive .NET SDK for integrating with Paystack payment gateway. This SDK provides a clean, type-safe interface for all major Paystack APIs.

## Features

- ✅ Transaction initialization and verification
- ✅ Customer management
- ✅ Charge authorization for saved cards
- ✅ Transfer and recipient management
- ✅ Bank listing and account resolution
- ✅ Dedicated Virtual Accounts (DVA) management
- ✅ Webhook event handling and verification
- ✅ Full async/await support
- ✅ Dependency injection integration
- ✅ Strongly typed models
- ✅ Configurable options

## Installation

```bash
dotnet add package ReenPaystack
```

## Quick Start

### 1. Configuration

Add your Paystack configuration to `appsettings.json`:

```json
{
  "Paystack": {
    "SecretKey": "sk_test_xxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
    "PublicKey": "pk_test_xxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
    "IsLiveMode": false,
    "WebhookSecret": "whsec_xxxxxxxxxxxxxxxxxxxxxxxxxx"
  }
}
```

### 2. Register Services

```csharp
// In Program.cs or Startup.cs
builder.Services.AddPaystackClient("your-secret-key");

// Add webhook support
builder.Services.AddPaystackWebhooks();
builder.Services.AddPaystackWebhookHandlers(); // Adds default handlers

// Or with configuration
builder.Services.AddPaystackClient(options =>
{
    options.SecretKey = "your-secret-key";
});
```

### 3. Use in Your Controller/Service

```csharp
public class PaymentController : ControllerBase
{
    private readonly IPaystackClient _paystackClient;

    public PaymentController(IPaystackClient paystackClient)
    {
        _paystackClient = paystackClient;
    }

    [HttpPost("initialize")]
    public async Task<IActionResult> InitializePayment([FromBody] PaymentRequest request)
    {
        var initializeRequest = new InitializeTransactionRequest
        {
            Email = request.Email,
            Amount = request.Amount * 100, // Convert to kobo
            Currency = "NGN",
            CallbackUrl = "https://yoursite.com/payment/callback"
        };

        var response = await _paystackClient.InitializeTransactionAsync(initializeRequest);
        
        if (response.Status)
        {
            return Ok(new { 
                authorization_url = response.Data?.AuthorizationUrl,
                reference = response.Data?.Reference
            });
        }
        
        return BadRequest(response.Message);
    }

    [HttpGet("verify/{reference}")]
    public async Task<IActionResult> VerifyPayment(string reference)
    {
        var response = await _paystackClient.VerifyTransactionAsync(reference);
        
        if (response.Status && response.Data?.Status == "success")
        {
            return Ok(new { message = "Payment verified successfully" });
        }
        
        return BadRequest("Payment verification failed");
    }
}
```

## API Reference

### Transactions

```csharp
// Initialize a transaction
var initRequest = new InitializeTransactionRequest
{
    Email = "customer@email.com",
    Amount = 10000, // Amount in kobo (₦100.00)
    Currency = "NGN"
};
var result = await _paystackClient.InitializeTransactionAsync(initRequest);

// Verify a transaction
var verification = await _paystackClient.VerifyTransactionAsync("reference");
```

### Customers

```csharp
// Create a customer
var customerRequest = new CreateCustomerRequest
{
    Email = "customer@email.com",
    FirstName = "John",
    LastName = "Doe"
};
var customer = await _paystackClient.CreateCustomerAsync(customerRequest);

// Get a customer
var existingCustomer = await _paystackClient.GetCustomerAsync("CUS_xxxxxxxxxxxxx");
```

### Charges

```csharp
// Charge a saved authorization
var chargeRequest = new ChargeAuthorizationRequest
{
    AuthorizationCode = "AUTH_xxxxxxxxxxxxx",
    Email = "customer@email.com",
    Amount = 10000 // Amount in kobo
};
var charge = await _paystackClient.ChargeAuthorizationAsync(chargeRequest);
```

### Transfers

```csharp
// Create a transfer recipient
var recipientRequest = new CreateTransferRecipientRequest
{
    Type = "nuban",
    Name = "John Doe",
    AccountNumber = "0123456789",
    BankCode = "058"
};
var recipient = await _paystackClient.CreateTransferRecipientAsync(recipientRequest);

// Initiate a transfer
var transferRequest = new InitiateTransferRequest
{
    Amount = 10000, // Amount in kobo
    Recipient = recipient.Data?.RecipientCode,
    Reason = "Payment for services"
};
var transfer = await _paystackClient.InitiateTransferAsync(transferRequest);
```

### Banks

```csharp
// Get list of banks
var banks = await _paystackClient.GetBanksAsync();

// Resolve account number
var account = await _paystackClient.ResolveAccountNumberAsync("0123456789", "058");
```

### Dedicated Virtual Accounts (DVA)

```csharp
// Create a customer first (required for DVA)
var customerRequest = new CreateCustomerRequest
{
    Email = "customer@email.com",
    FirstName = "John",
    LastName = "Doe",
    Phone = "+2348012345678"
};
var customer = await _paystackClient.CreateCustomerAsync(customerRequest);

// Create a dedicated virtual account
var dvaRequest = new CreateDedicatedAccountRequest
{
    Customer = customer.Data?.CustomerCode, // Use customer code from above
    PreferredBank = "wema-bank", // Optional: specify preferred bank
    FirstName = "John",
    LastName = "Doe",
    Phone = "+2348012345678"
};
var dedicatedAccount = await _paystackClient.CreateDedicatedAccountAsync(dvaRequest);

// Get account details
if (dedicatedAccount.Status && dedicatedAccount.Data != null)
{
    var accountNumber = dedicatedAccount.Data.AccountNumber;
    var accountName = dedicatedAccount.Data.AccountName;
    var bankName = dedicatedAccount.Data.Bank?.Name;
    
    Console.WriteLine($"Account: {accountNumber} - {accountName} ({bankName})");
}

// List all dedicated accounts
var accounts = await _paystackClient.ListDedicatedAccountsAsync(
    active: true, 
    customer: customer.Data?.CustomerCode
);

// Get a specific dedicated account
var specificAccount = await _paystackClient.GetDedicatedAccountAsync("12345");

// Create DVA with split transaction support
var splitRequest = new SplitDedicatedAccountRequest
{
    Customer = customer.Data?.CustomerCode,
    Subaccount = "ACCT_xxxxxxxxxxxxx", // Your subaccount code
    SplitCode = "SPL_xxxxxxxxxxxxx"   // Your split code
};
var splitAccount = await _paystackClient.SplitDedicatedAccountTransactionAsync(splitRequest);

// Remove split from DVA
var removeResult = await _paystackClient.RemoveSplitFromDedicatedAccountAsync("1234567890");

// Deactivate a dedicated account
var deactivateRequest = new DeactivateDedicatedAccountRequest
{
    AccountNumber = "1234567890"
};
var deactivateResult = await _paystackClient.DeactivateDedicatedAccountAsync(deactivateRequest);
```

## Complete DVA Workflow Example

Here's a complete example showing the typical workflow for creating and managing dedicated virtual accounts:

```csharp
public class DVAService
{
    private readonly IPaystackClient _paystackClient;

    public DVAService(IPaystackClient paystackClient)
    {
        _paystackClient = paystackClient;
    }

    public async Task<(string AccountNumber, string AccountName, string BankName)> CreateDedicatedAccountForCustomerAsync(
        string email, 
        string firstName, 
        string lastName, 
        string phone,
        string? preferredBank = null)
    {
        try
        {
            // Step 1: Create customer
            var customerRequest = new CreateCustomerRequest
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Phone = phone
            };

            var customerResponse = await _paystackClient.CreateCustomerAsync(customerRequest);
            
            if (!customerResponse.Status || customerResponse.Data == null)
            {
                throw new Exception($"Failed to create customer: {customerResponse.Message}");
            }

            // Step 2: Create dedicated virtual account
            var dvaRequest = new CreateDedicatedAccountRequest
            {
                Customer = customerResponse.Data.CustomerCode,
                PreferredBank = preferredBank,
                FirstName = firstName,
                LastName = lastName,
                Phone = phone
            };

            var dvaResponse = await _paystackClient.CreateDedicatedAccountAsync(dvaRequest);
            
            if (!dvaResponse.Status || dvaResponse.Data == null)
            {
                throw new Exception($"Failed to create DVA: {dvaResponse.Message}");
            }

            return (
                dvaResponse.Data.AccountNumber,
                dvaResponse.Data.AccountName,
                dvaResponse.Data.Bank?.Name ?? "Unknown Bank"
            );
        }
        catch (Exception ex)
        {
            throw new Exception($"DVA creation failed: {ex.Message}", ex);
        }
    }

    public async Task<List<DedicatedAccountResponse>> GetCustomerDVAsAsync(string customerCode)
    {
        var response = await _paystackClient.ListDedicatedAccountsAsync(
            active: true,
            customer: customerCode
        );

        return response.Status && response.Data != null 
            ? response.Data.ToList() 
            : new List<DedicatedAccountResponse>();
    }
}
```

## Webhook Handling

The SDK provides comprehensive webhook handling capabilities for processing Paystack events securely.

### Basic Webhook Setup

```csharp
[ApiController]
[Route("api/webhooks")]
public class WebhookController : ControllerBase
{
    private readonly IPaystackWebhookDispatcher _webhookDispatcher;
    private readonly IConfiguration _configuration;

    public WebhookController(IPaystackWebhookDispatcher webhookDispatcher, IConfiguration configuration)
    {
        _webhookDispatcher = webhookDispatcher;
        _configuration = configuration;
    }

    [HttpPost("paystack")]
    public async Task<IActionResult> PaystackWebhook()
    {
        var payload = await new StreamReader(Request.Body).ReadToEndAsync();
        var signature = Request.Headers["X-Paystack-Signature"].FirstOrDefault();
        var webhookSecret = _configuration["Paystack:WebhookSecret"];

        if (string.IsNullOrEmpty(signature) || string.IsNullOrEmpty(webhookSecret))
        {
            return BadRequest("Missing signature or webhook secret");
        }

        var processed = await _webhookDispatcher.ProcessWebhookAsync(payload, signature, webhookSecret);
        
        return processed ? Ok() : BadRequest("Webhook processing failed");
    }
}
```

### Manual Webhook Processing

```csharp
public class PaymentService
{
    private readonly IPaystackWebhookService _webhookService;

    public PaymentService(IPaystackWebhookService webhookService)
    {
        _webhookService = webhookService;
    }

    public async Task<bool> ProcessChargeSuccessWebhook(string payload, string signature, string secret)
    {
        return await _webhookService.ProcessWebhookAsync<ChargeSuccessEvent>(
            payload, 
            signature, 
            secret,
            async (webhookEvent) =>
            {
                var charge = webhookEvent.Data;
                
                // Update payment status in database
                await UpdatePaymentStatus(charge.Reference, "success", charge.Amount);
                
                // Send confirmation email
                await SendPaymentConfirmation(charge.Customer?.Email, charge);
                
                // Fulfill order
                await ProcessOrderFulfillment(charge.Reference);
                
                Console.WriteLine($"Payment processed: {charge.Reference}");
            });
    }
}
```

### Custom Webhook Handlers

Create custom handlers by implementing the webhook interfaces:

```csharp
public class CustomChargeSuccessHandler : IPaystackWebhookHandler<ChargeSuccessEvent>
{
    private readonly ILogger<CustomChargeSuccessHandler> _logger;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IEmailService _emailService;

    public string EventType => PaystackWebhookEvents.ChargeSuccess;

    public CustomChargeSuccessHandler(
        ILogger<CustomChargeSuccessHandler> logger,
        IPaymentRepository paymentRepository,
        IEmailService emailService)
    {
        _logger = logger;
        _paymentRepository = paymentRepository;
        _emailService = emailService;
    }

    public async Task HandleAsync(PaystackWebhookEvent<ChargeSuccessEvent> webhookEvent)
    {
        var charge = webhookEvent.Data;
        
        try
        {
            // Update payment in database
            var payment = await _paymentRepository.GetByReferenceAsync(charge.Reference);
            if (payment != null)
            {
                payment.Status = "completed";
                payment.PaidAt = charge.PaidAt;
                payment.PaystackFees = charge.Fees ?? 0;
                await _paymentRepository.UpdateAsync(payment);
            }

            // Send confirmation email
            await _emailService.SendPaymentConfirmationAsync(charge.Customer?.Email, charge);
            
            _logger.LogInformation("Successfully processed charge: {Reference}", charge.Reference);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing charge success webhook: {Reference}", charge.Reference);
            throw;
        }
    }
}

// Register custom handler
builder.Services.AddPaystackWebhookHandler<ChargeSuccessEvent, CustomChargeSuccessHandler>();
```

### DVA Webhook Handling

```csharp
public class DVAPaymentHandler : IPaystackWebhookHandler<ChargeSuccessEvent>
{
    public string EventType => PaystackWebhookEvents.ChargeSuccess;

    public async Task HandleAsync(PaystackWebhookEvent<ChargeSuccessEvent> webhookEvent)
    {
        var charge = webhookEvent.Data;
        
        // Check if payment came through DVA
        if (charge.Channel == "dedicated_nuban")
        {
            // Handle DVA payment
            Console.WriteLine($"DVA Payment received: ₦{charge.Amount / 100:F2} from {charge.Customer?.Email}");
            
            // Process DVA-specific logic
            await ProcessDVAPayment(charge);
        }
    }

    private async Task ProcessDVAPayment(ChargeSuccessEvent charge)
    {
        // Your DVA payment processing logic
        await Task.CompletedTask;
    }
}
```

### Webhook Event Types

The SDK supports all major Paystack webhook events:

```csharp
public static class PaystackWebhookEvents
{
    public const string ChargeSuccess = "charge.success";
    public const string ChargeFailed = "charge.failed";
    public const string TransferSuccess = "transfer.success";
    public const string TransferFailed = "transfer.failed";
    public const string DedicatedAccountAssignSuccess = "dedicatedaccount.assign.success";
    public const string CustomerIdentificationSuccess = "customeridentification.success";
    public const string InvoicePaymentSuccess = "invoice.payment_success";
    public const string SubscriptionCreate = "subscription.create";
    // ... and more
}
```

### Webhook Security

The SDK automatically verifies webhook signatures using HMAC-SHA512:

```csharp
public class WebhookSecurityService
{
    private readonly IPaystackWebhookService _webhookService;

    public bool VerifyWebhook(string payload, string signature, string secret)
    {
        return _webhookService.VerifySignature(payload, signature, secret);
    }

    public PaystackWebhookEvent ParseSafeWebhook(string payload, string signature, string secret)
    {
        if (!_webhookService.VerifySignature(payload, signature, secret))
        {
            throw new UnauthorizedAccessException("Invalid webhook signature");
        }

        return _webhookService.ParseWebhookEvent(payload);
    }
}
```

## Error Handling

All API methods return a `PaystackResponse<T>` object with the following structure:

```csharp
public class PaystackResponse<T>
{
    public bool Status { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
}
```

Example error handling:

```csharp
var response = await _paystackClient.InitializeTransactionAsync(request);

if (!response.Status)
{
    // Handle error
    Console.WriteLine($"Error: {response.Message}");
    return;
}

// Use response.Data
var authUrl = response.Data?.AuthorizationUrl;
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License.