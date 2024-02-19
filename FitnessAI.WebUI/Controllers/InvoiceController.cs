using FitnessAI.Application.GymInvoices.Queries.GetPdfGymInvoice;
using FitnessAI.WebUI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebUI.Controllers;

[Authorize]
public class InvoiceController : BaseController
{
    private readonly ILogger _logger;

    public InvoiceController(ILogger<InvoiceController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> InvoiceToPdf(int id)
    {
        try
        {
            var invoicePdfVm = await Mediator.Send(new GetPdfGymInvoiceQuery
            {
                Id = id
            });

            TempData.Put(invoicePdfVm.Handle, invoicePdfVm.PdfContent);

            return Json(new
            {
                success = true,
                fileGuid = invoicePdfVm.Handle,
                fileName = invoicePdfVm.FileName
            });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, null);
            return Json(new { success = false });
        }
    }

    public IActionResult DownloadInvoicePdf(string fileGuid, string fileName)
    {
        if (TempData[fileGuid] == null)
            throw new Exception("Błąd przy próbie eksportu faktury do PDF.");

        return File(TempData.Get<byte[]>(fileGuid), "application/pdf", fileName);
    }
}