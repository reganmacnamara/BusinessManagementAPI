using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using MacsBusinessManagementAPI.Infrastructure.Services.Pdf;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Invoices.GetInvoicePdf;

public class GetInvoicePdfHandler(IPdfService pdfService, SQLContext context) : IUseCaseHandler<GetInvoicePdfRequest>
{
    public async Task<IResult> HandleAsync(GetInvoicePdfRequest request, CancellationToken cancellationToken)
    {
        var _Invoice = await context.GetEntities<Invoice>()
            .AsNoTracking()
            .Include(i => i.Client)
            .Where(i => i.InvoiceID == request.InvoiceID)
            .SingleOrDefaultAsync(cancellationToken);

        if (_Invoice is null)
            return Results.NotFound($"Invoice {request.InvoiceID} could not be found.");

        var _InvoiceItems = await context.GetEntities<InvoiceItem>()
            .AsNoTracking()
            .Include(ii => ii.Product)
            .Where(ii => ii.InvoiceID == request.InvoiceID)
            .ToListAsync(cancellationToken);

        var _PdfBytes = pdfService.GenerateInvoicePdf(_Invoice, _InvoiceItems);

        return Results.File(_PdfBytes, "application/pdf", $"Invoice_{_Invoice.InvoiceRef}.pdf");
    }
}
