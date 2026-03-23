using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Receipts.DeleteReceipt;

public class DeleteReceiptRequest : IUseCaseRequest
{
    public long ReceiptID { get; set; }
}
