using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Receipts.GetReceipt;

public class GetReceiptRequest : IUseCaseRequest
{
    public long ReceiptID { get; set; }
}
