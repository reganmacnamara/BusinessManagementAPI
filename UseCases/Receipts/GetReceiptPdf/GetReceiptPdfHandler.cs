using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using MacsBusinessManagementAPI.Services.Pdf;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Receipts.GetReceiptPdf;

public class GetReceiptPdfHandler(IPdfService pdfService, SQLContext context) : IUseCaseHandler<GetReceiptPdfRequest>
{
    public async Task<IResult> HandleAsync(GetReceiptPdfRequest request, CancellationToken cancellationToken)
    {
        var _Receipt = await context.GetEntities<Receipt>()
            .AsNoTracking()
            .Include(r => r.Client)
            .SingleOrDefaultAsync(r => r.ReceiptID == request.ReceiptID, cancellationToken);

        if (_Receipt is null)
            return Results.NotFound($"Receipt {request.ReceiptID} could not be found.");

        var _ReceiptItems = await context.GetEntities<ReceiptItem>()
            .AsNoTracking()
            .Include(ri => ri.Invoice)
            .Where(ri => ri.ReceiptID == request.ReceiptID)
            .ToListAsync(cancellationToken);

        var _PdfBytes = pdfService.GenerateReceiptPdf(_Receipt, _ReceiptItems);

        return Results.File(_PdfBytes, "application/pdf", $"Receipt_{_Receipt.ReceiptRef}.pdf");
    }
}
