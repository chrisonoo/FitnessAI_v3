using FitnessAI.Application.Common.Models.Payments;

namespace FitnessAI.Application.Common.Interfaces;

public interface IPrzelewy24
{
    Task<P24TestAccessResponse> TestConnectionAsync();
    Task<P24TransactionResponse> NewTransactionAsync(P24TransactionRequest data);
    Task<P24TransactionVerifyResponse> TransactionVerifyAsync(P24TransactionVerifyRequest data);
}