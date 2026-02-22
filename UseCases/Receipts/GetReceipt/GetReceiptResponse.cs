using BusinessManagementAPI.Entities;

namespace BusinessManagementAPI.UseCases.Receipts.GetReceipt;

public class GetReceiptResponse
{
    public Receipt Receipt { get; set; }

    public List<ReceiptItem> ReceiptItems { get; set; } = [];
}
