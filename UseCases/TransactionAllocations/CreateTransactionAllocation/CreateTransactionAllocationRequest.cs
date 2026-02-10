namespace BusinessManagementAPI.UseCases.TransactionAllocations.CreateTransactionAllocation;

public class CreateTransactionAllocationRequest
{
    public long AllocatingID { get; set; }

    public long RecievingID { get; set; }

    public decimal AllocationValue { get; set; }

    public DateTime AllocationDate { get; set; }
}
