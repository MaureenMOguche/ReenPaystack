using ReenPaystack.Models;

namespace ReenPaystack.Interfaces;

public interface IPaystackClient
{
    Task<PaystackResponse<TransactionResponse>> InitializeTransactionAsync(InitializeTransactionRequest request);
    Task<PaystackResponse<TransactionResponse>> VerifyTransactionAsync(string reference);
    Task<PaystackResponse<CustomerResponse>> CreateCustomerAsync(CreateCustomerRequest request);
    Task<PaystackResponse<CustomerResponse>> GetCustomerAsync(string customerCode);
    Task<PaystackResponse<ChargeResponse>> ChargeAuthorizationAsync(ChargeAuthorizationRequest request);
    Task<PaystackResponse<TransferResponse>> InitiateTransferAsync(InitiateTransferRequest request);
    Task<PaystackResponse<TransferRecipientResponse>> CreateTransferRecipientAsync(CreateTransferRecipientRequest request);
    Task<PaystackResponse<BankResponse[]>> GetBanksAsync();
    Task<PaystackResponse<ResolveAccountResponse>> ResolveAccountNumberAsync(string accountNumber, string bankCode);
    
    Task<PaystackResponse<DedicatedAccountResponse>> CreateDedicatedAccountAsync(CreateDedicatedAccountRequest request);
    Task<PaystackResponse<DedicatedAccountResponse>> CreateDedicatedAccountAsync(CreateCustomerRequest request, string preferredBank);
    
    Task<PaystackResponse<DedicatedAccountResponse[]>> ListDedicatedAccountsAsync(bool? active = null, string? currency = null, string? providedBank = null, string? bankId = null, string? customer = null);
    Task<PaystackResponse<DedicatedAccountResponse>> GetDedicatedAccountAsync(string accountId);
    Task<PaystackResponse<PaystackBaseResponse>> DeactivateDedicatedAccountAsync(DeactivateDedicatedAccountRequest request);
    Task<PaystackResponse<DedicatedAccountResponse>> SplitDedicatedAccountTransactionAsync(SplitDedicatedAccountRequest request);
    Task<PaystackResponse<DedicatedAccountResponse>> RemoveSplitFromDedicatedAccountAsync(string accountNumber);
}