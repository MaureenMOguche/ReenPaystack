using System.Text;
using System.Text.Json;
using ReenPaystack.Interfaces;
using ReenPaystack.Models;

namespace ReenPaystack.Services;

public class PaystackClient : IPaystackClient
{
    private readonly HttpClient _httpClient;
    private readonly string _secretKey;
    private readonly JsonSerializerOptions _jsonOptions;
    private const string BaseUrl = "https://api.paystack.co";

    public PaystackClient(HttpClient httpClient, string secretKey)
    {
        _httpClient = httpClient;
        _secretKey = secretKey;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        ConfigureHttpClient();
    }

    private void ConfigureHttpClient()
    {
        _httpClient.BaseAddress = new Uri(BaseUrl);
        _httpClient.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _secretKey);
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "ReenPaystack/1.0.0");
    }

    public async Task<PaystackResponse<TransactionResponse>> InitializeTransactionAsync(InitializeTransactionRequest request)
    {
        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync("/transaction/initialize", content);
        var responseJson = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<PaystackResponse<TransactionResponse>>(responseJson, _jsonOptions)!;
    }

    public async Task<PaystackResponse<TransactionResponse>> VerifyTransactionAsync(string reference)
    {
        var response = await _httpClient.GetAsync($"/transaction/verify/{reference}");
        var responseJson = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<PaystackResponse<TransactionResponse>>(responseJson, _jsonOptions)!;
    }

    public async Task<PaystackResponse<CustomerResponse>> CreateCustomerAsync(CreateCustomerRequest request)
    {
        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync("/customer", content);
        var responseJson = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<PaystackResponse<CustomerResponse>>(responseJson, _jsonOptions)!;
    }

    public async Task<PaystackResponse<CustomerResponse>> GetCustomerAsync(string customerCode)
    {
        var response = await _httpClient.GetAsync($"/customer/{customerCode}");
        var responseJson = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<PaystackResponse<CustomerResponse>>(responseJson, _jsonOptions)!;
    }

    public async Task<PaystackResponse<ChargeResponse>> ChargeAuthorizationAsync(ChargeAuthorizationRequest request)
    {
        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync("/transaction/charge_authorization", content);
        var responseJson = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<PaystackResponse<ChargeResponse>>(responseJson, _jsonOptions)!;
    }

    public async Task<PaystackResponse<TransferResponse>> InitiateTransferAsync(InitiateTransferRequest request)
    {
        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync("/transfer", content);
        var responseJson = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<PaystackResponse<TransferResponse>>(responseJson, _jsonOptions)!;
    }

    public async Task<PaystackResponse<TransferRecipientResponse>> CreateTransferRecipientAsync(CreateTransferRecipientRequest request)
    {
        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync("/transferrecipient", content);
        var responseJson = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<PaystackResponse<TransferRecipientResponse>>(responseJson, _jsonOptions)!;
    }

    public async Task<PaystackResponse<BankResponse[]>> GetBanksAsync()
    {
        var response = await _httpClient.GetAsync("/bank");
        var responseJson = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<PaystackResponse<BankResponse[]>>(responseJson, _jsonOptions)!;
    }

    public async Task<PaystackResponse<ResolveAccountResponse>> ResolveAccountNumberAsync(string accountNumber, string bankCode)
    {
        var response = await _httpClient.GetAsync($"/bank/resolve?account_number={accountNumber}&bank_code={bankCode}");
        var responseJson = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<PaystackResponse<ResolveAccountResponse>>(responseJson, _jsonOptions)!;
    }

    public async Task<PaystackResponse<DedicatedAccountResponse>> CreateDedicatedAccountAsync(CreateDedicatedAccountRequest request)
    {
        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync("/dedicated_account", content);
        var responseJson = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<PaystackResponse<DedicatedAccountResponse>>(responseJson, _jsonOptions)!;
    }
    
    public async Task<PaystackResponse<DedicatedAccountResponse>> CreateDedicatedAccountAsync(CreateCustomerRequest request, string preferredBank)
    {
        var customer = await CreateCustomerAsync(request);

        if (!customer.Status || customer.Data == null)
            throw new Exception("Customer not found");
        
        var dvaRequest = new CreateDedicatedAccountRequest()
        {
            Customer = customer.Data.CustomerCode,
            PreferredBank = preferredBank,
            Phone = customer.Data.Phone
        };
        
        var json = JsonSerializer.Serialize(dvaRequest, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync("/dedicated_account", content);
        var responseJson = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<PaystackResponse<DedicatedAccountResponse>>(responseJson, _jsonOptions)!;
    }

    public async Task<PaystackResponse<DedicatedAccountResponse[]>> ListDedicatedAccountsAsync(bool? active = null, string? currency = null, string? providedBank = null, string? bankId = null, string? customer = null)
    {
        var queryParams = new List<string>();
        
        if (active.HasValue)
            queryParams.Add($"active={active.Value.ToString().ToLower()}");
        if (!string.IsNullOrEmpty(currency))
            queryParams.Add($"currency={currency}");
        if (!string.IsNullOrEmpty(providedBank))
            queryParams.Add($"provider_bank={providedBank}");
        if (!string.IsNullOrEmpty(bankId))
            queryParams.Add($"bank_id={bankId}");
        if (!string.IsNullOrEmpty(customer))
            queryParams.Add($"customer={customer}");

        var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : string.Empty;
        
        var response = await _httpClient.GetAsync($"/dedicated_account{queryString}");
        var responseJson = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<PaystackResponse<DedicatedAccountResponse[]>>(responseJson, _jsonOptions)!;
    }

    public async Task<PaystackResponse<DedicatedAccountResponse>> GetDedicatedAccountAsync(string accountId)
    {
        var response = await _httpClient.GetAsync($"/dedicated_account/{accountId}");
        var responseJson = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<PaystackResponse<DedicatedAccountResponse>>(responseJson, _jsonOptions)!;
    }

    public async Task<PaystackResponse<PaystackBaseResponse>> DeactivateDedicatedAccountAsync(DeactivateDedicatedAccountRequest request)
    {
        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.DeleteAsync("/dedicated_account/deactivate");
        var responseJson = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<PaystackResponse<PaystackBaseResponse>>(responseJson, _jsonOptions)!;
    }

    public async Task<PaystackResponse<DedicatedAccountResponse>> SplitDedicatedAccountTransactionAsync(SplitDedicatedAccountRequest request)
    {
        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync("/dedicated_account/split", content);
        var responseJson = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<PaystackResponse<DedicatedAccountResponse>>(responseJson, _jsonOptions)!;
    }

    public async Task<PaystackResponse<DedicatedAccountResponse>> RemoveSplitFromDedicatedAccountAsync(string accountNumber)
    {
        var json = JsonSerializer.Serialize(new { account_number = accountNumber }, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync("/dedicated_account/remove_split", content);
        var responseJson = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<PaystackResponse<DedicatedAccountResponse>>(responseJson, _jsonOptions)!;
    }
}