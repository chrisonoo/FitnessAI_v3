﻿using FitnessAI.Application.Common.Interfaces;
using FitnessAI.Application.Common.Models;
using FitnessAI.Application.Tickets.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessAI.Application.Tickets.Queries.GetPdfTicket;

public class GetPdfTicketQueryHandler : IRequestHandler<GetPdfTicketQuery, TicketPdfVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IPdfFileGenerator _pdfFileGenerator;
    private readonly IQrCodeGenerator _qrCodeGenerator;

    public GetPdfTicketQueryHandler(
        IApplicationDbContext context,
        IPdfFileGenerator pdfFileGenerator,
        IQrCodeGenerator qrCodeGenerator)
    {
        _context = context;
        _pdfFileGenerator = pdfFileGenerator;
        _qrCodeGenerator = qrCodeGenerator;
    }

    public async Task<TicketPdfVm> Handle(GetPdfTicketQuery request, CancellationToken cancellationToken)
    {
        var vm = new TicketPdfVm();

        vm.Handle = Guid.NewGuid().ToString();

        var ticket = (await _context
                .Tickets
                .AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == request.TicketId && x.UserId == request.UserId))
            .ToPrintTicketDto();

        if (ticket == null)
            return null;

        ticket.QrCodeId = _qrCodeGenerator.Get(request.TicketId);

        vm.PdfContent = await _pdfFileGenerator.GetAsync(new FileGeneratorParams
            { Context = request.Context, Model = ticket, ViewTemplate = "TicketPreview" });

        vm.FileName = $"Karnet_{ticket.Id}.pdf";

        return vm;
    }
}