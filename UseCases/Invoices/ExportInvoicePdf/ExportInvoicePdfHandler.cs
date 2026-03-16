using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.Services;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Invoices.ExportInvoicePdf;

public class ExportInvoicePdfHandler(IPdfService pdfService, IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> ExportInvoicePdf(ExportInvoicePdfRequest request)
    {
        var _Invoice = m_Context.GetEntities<Invoice>()
            .AsNoTracking()
            .Include(i => i.Client)
            .Where(i => i.InvoiceID == request.InvoiceID)
            .SingleOrDefault();

        if (_Invoice is null)
            return Results.NotFound($"Invoice {request.InvoiceID} could not be found.");

        var _InvoiceItems = m_Context.GetEntities<InvoiceItem>()
            .AsNoTracking()
            .Include(ii => ii.Product)
            .Where(ii => ii.InvoiceID == request.InvoiceID)
            .ToList();

        var _PdfBytes = pdfService.GenerateInvoicePdf(_Invoice, _InvoiceItems);

        return Results.File(_PdfBytes, "application/pdf", $"Invoice_{_Invoice.InvoiceRef}.pdf");
    }
}
