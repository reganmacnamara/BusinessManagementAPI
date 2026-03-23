using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Receipts.DeleteReceiptItem;

public class DeleteReceiptItemRequest : IUseCaseRequest
{
    public long ReceiptItemID { get; set; }
}
