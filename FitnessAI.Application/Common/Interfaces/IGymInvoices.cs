using FitnessAI.Application.GymInvoices.Queries.GetPdfGymInvoice;

namespace FitnessAI.Application.Common.Interfaces;

public interface IGymInvoices
{
    Task AddInvoice(string ticketId, string userId = null);
    Task<InvoicePdfVm> GetPdfInvoice(int id);
}