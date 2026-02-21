using BusinessManagementAPI.Entities;

namespace BusinessManagementAPI.UseCases.TransactionAllocations.GetAllocationsByTransaction;


public class GetAllocationsByTransactionResponse
{
    public List<TransactionAllocation> TransactionAllocations { get; set; }
}
