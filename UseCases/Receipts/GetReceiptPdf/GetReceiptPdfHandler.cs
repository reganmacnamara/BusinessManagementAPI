using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.Services.Pdf;
using MacsBusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Receipts.GetReceiptPdf;

public class GetReceiptPdfHandler(IPdfService pdfService, IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<GetReceiptPdfRequest>
{
    public async Task<IResult> HandleAsync(GetReceiptPdfRequest request, CancellationToken cancellationToken)
    {
        var _Receipt = m_Context.GetEntities<Receipt>()
            .AsNoTracking()
            .Include(r => r.Client)
            .Where(r => r.ReceiptID == request.ReceiptID)
            .SingleOrDefault();

        if (_Receipt is null)
            return Results.NotFound($"Receipt {request.ReceiptID} could not be found.");

        var _ReceiptItems = m_Context.GetEntities<ReceiptItem>()
            .AsNoTracking()
            .Include(ri => ri.Invoice)
            .Where(ri => ri.ReceiptID == request.ReceiptID)
            .ToList();

        var _PdfBytes = pdfService.GenerateReceiptPdf(_Receipt, _ReceiptItems);

        return Results.File(_PdfBytes, "application/pdf", $"Receipt_{_Receipt.ReceiptRef}.pdf");
    }
}
