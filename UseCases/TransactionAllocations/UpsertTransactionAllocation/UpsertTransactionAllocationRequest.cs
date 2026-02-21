namespace BusinessManagementAPI.UseCases.TransactionAllocations.UpsertTransactionAllocation;

public class UpsertTransactionAllocationRequest
{
    public long TransactionAllocationID { get; set; }

    public long AllocatingID { get; set; }

    public long RecievingID { get; set; }

    public decimal AllocationValue { get; set; }

    public DateTime AllocationDate { get; set; }
}
