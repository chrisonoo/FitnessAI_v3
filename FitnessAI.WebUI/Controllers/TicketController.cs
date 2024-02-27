using DataTables.AspNet.Core;
using FitnessAI.Application.Clients.Queries.GetClient;
using FitnessAI.Application.Tickets.Commands.AddTicket;
using FitnessAI.Application.Tickets.Queries.GetAddTicket;
using FitnessAI.Application.Tickets.Queries.GetClientsTickets;
using FitnessAI.Application.Tickets.Queries.GetPdfTicket;
using FitnessAI.Application.Tickets.Queries.GetPrintTicket;
using FitnessAI.WebUI.Extensions;
using FitnessAI.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace FitnessAI.WebUI.Controllers;

[Authorize]
public class TicketController : BaseController
{
    private readonly IConfiguration _configuration;
    private readonly IStringLocalizer<TicketController> _localizer;
    private readonly ILogger _logger;

    public TicketController(
        IConfiguration configuration,
        ILogger<TicketController> logger,
        IStringLocalizer<TicketController> localizer)
    {
        _configuration = configuration;
        _logger = logger;
        _localizer = localizer;
    }

    public async Task<IActionResult> Tickets()
    {
        var isUserDataCompleted =
            !string.IsNullOrWhiteSpace((await Mediator.Send(new GetClientQuery { UserId = LoggedUserId })).FirstName);

        var ticketsViewModel = new TicketsViewModel
        {
            IsUserDataCompleted = isUserDataCompleted
        };
        
        // Test płatności
        var newTicketSessionId = TempData["NewTicketSessionId"];
        if (newTicketSessionId != null)
            ticketsViewModel.NewTicketSessionId = newTicketSessionId.ToString();
        
        return View(ticketsViewModel);
    }

    public async Task<IActionResult> TicketsDataTable(IDataTablesRequest request)
    {
        var tickets = await Mediator.Send(new GetClientsTicketsQuery
        {
            UserId = LoggedUserId,
            PageSize = request.Length,
            SearchValue = request.Search.Value,
            PageNumber = request.GetPageNumber(),
            OrderInfo = request.GetOrderInfo()
        });

        var response = request.GetResponse(tickets);
        return response;
    }

    public async Task<IActionResult> AddTicket()
    {
        return View(await Mediator.Send(new GetAddTicketQuery ()));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddTicket(AddTicketVm vm)
    {
        var result = await Mediator.Send(new AddTicketCommand
        {
            StartDate = vm.Ticket.StartDate,
            TicketTypeId = vm.Ticket.TicketTypeId,
            UserId = LoggedUserId,
            Price = vm.Ticket.Price
        });

        TempData["Success"] = "Nowy karnet został utworzony, oczekiwanie na zweryfikowanie płatności.";
        
        // TODO:: Uwzględnić tłumacznie przy zmianie języka
        // TempData["Success"] = _localizer["TicketCreated"].Value;
        
        // Symulacja potwierdzenia płatności od systemu obsługującego płatności
        TempData["NewTicketSessionId"] = result;

        return RedirectToAction(nameof(Tickets));
    }

    public async Task<IActionResult> TicketPreview(string id)
    {
        var ticket = await Mediator.Send(new GetPrintTicketQuery
        {
            TicketId = id,
            UserId = LoggedUserId
        });

        return View(ticket);
    }

    public async Task<IActionResult> TicketToPdf(string id)
    {
        try
        {
            var ticketPdfVm = await Mediator.Send(new GetPdfTicketQuery
            {
                TicketId = id,
                UserId = LoggedUserId,
                Context = ControllerContext
            });

            TempData.Put(ticketPdfVm.Handle, ticketPdfVm.PdfContent);

            return Json(new
            {
                success = true,
                fileGuid = ticketPdfVm.Handle,
                fileName = ticketPdfVm.FileName
            });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, null);
            return Json(new { success = false });
        }
    }

    public IActionResult DownloadTicketPdf(string fileGuid, string fileName)
    {
        if (TempData[fileGuid] == null)
            throw new Exception("Błąd przy próbie eksportu karnetu do PDF.");

        return File(TempData.Get<byte[]>(fileGuid), "application/pdf", fileName);
    }

    public async Task<IActionResult> PrintTicket(string id)
    {
        var ticket = await Mediator.Send(new GetPrintTicketQuery
        {
            TicketId = id,
            UserId = LoggedUserId
        });

        return View("TicketPreview", ticket);
    }
}