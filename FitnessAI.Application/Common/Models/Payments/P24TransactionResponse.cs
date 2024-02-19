namespace FitnessAI.Application.Common.Models.Payments;

public class P24TransactionResponse
{
    public TransactionData Data { get; set; }
    public int ResponseCode { get; set; }

    public string Error { get; set; }
    public int Code { get; set; }

    public class TransactionData
    {
        public string Token { get; set; }
    }
}