using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Receipts.GetClientReceipts;

public class GetClientReceiptsRequest : IUseCaseRequest
{
    public long ClientID { get; set; }
}
