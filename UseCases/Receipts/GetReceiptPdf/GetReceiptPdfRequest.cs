using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Receipts.GetReceiptPdf;

public class GetReceiptPdfRequest : IUseCaseRequest
{
    public long ReceiptID { get; set; }
}
