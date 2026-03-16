using BusinessManagementAPI.Entities;

namespace BusinessManagementAPI.UseCases.Receipts.GetReceipt;

public class GetReceiptResponse
{
    public Receipt Receipt { get; set; } = default!;

    public List<ReceiptItem> ReceiptItems { get; set; } = [];
}
