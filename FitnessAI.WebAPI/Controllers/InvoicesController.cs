using FitnessAI.Application.Invoices.Commands.AddInvoice;
using FitnessAI.Application.Invoices.Commands.DeleteInvoice;
using FitnessAI.Application.Invoices.Commands.EditInvoice;
using FitnessAI.Application.Invoices.Queries.GetInvoice;
using FitnessAI.Application.Invoices.Queries.GetInvoices;
using FitnessAI.Application.Invoices.Queries.GetPdfInvoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessAI.WebAPI.Controllers;

[ApiVersion("1")]
[ApiExplorerSettings(GroupName = "v1")]
[Authorize]
public class InvoicesController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var invoices = await Mediator.Send(new GetInvoicesQuery
        {
            UserId = UserId
        });

        return Ok(invoices);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var invoice = await Mediator.Send(new GetInvoiceQuery
        {
            UserId = UserId,
            Id = id
        });

        if (invoice == null)
            return NotFound();

        return Ok(invoice);
    }

    [MapToApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    [HttpGet]
    public async Task<IActionResult> GetAllv2()
    {
        await Task.CompletedTask;

        return Ok(new List<InvoiceBasicsDto>
        {
            new()
            {
                Id = 100, CreatedDate = new DateTime(2000, 1, 1), Title = "1", UserId = "1", UserName = "Test",
                Value = 1
            }
        });
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddInvoiceCommand command)
    {
        command.UserId = UserId;
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Edit(EditInvoiceCommand command)
    {
        command.UserId = UserId;
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteInvoiceCommand command)
    {
        command.UserId = UserId;
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpGet("pdf/{id}")]
    public async Task<IActionResult> GetPdf(int id)
    {
        var vm = await Mediator.Send(new GetPdfInvoiceQuery
        {
            UserId = UserId,
            InvoiceId = id,
            Context = ControllerContext
        });

        if (vm == null)
            return NotFound();

        return Ok(vm);
    }
}