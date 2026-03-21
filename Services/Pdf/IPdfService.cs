using MacsBusinessManagementAPI.Entities;

namespace MacsBusinessManagementAPI.Services.Pdf;

public interface IPdfService
{
    byte[] GenerateInvoicePdf(Invoice invoice, List<InvoiceItem> items);
    byte[] GenerateReceiptPdf(Receipt receipt, List<ReceiptItem> items);
}
