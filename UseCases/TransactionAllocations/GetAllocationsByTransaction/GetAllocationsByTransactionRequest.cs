namespace BusinessManagementAPI.UseCases.TransactionAllocations.GetAllocationsByTransaction;

public class GetAllocationsByTransactionRequest
{
    public long TransactionID { get; set; }

    public bool IsReciever { get; set; }
}
