namespace BusinessManagementAPI.UseCases.TransactionAllocations.GetTransactionAllocationsByTransaction;

public class GetAllocationsByTransactionRequest
{
    public long TransactionID { get; set; }

    public bool IsReciever { get; set; }
}
