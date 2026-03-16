using BusinessManagementAPI.Entities;

namespace BusinessManagementAPI.Services;

public interface IPdfService
{
    byte[] GenerateInvoicePdf(Invoice invoice, List<InvoiceItem> items);
    byte[] GenerateReceiptPdf(Receipt receipt, List<ReceiptItem> items);
}
