using BusinessManagementAPI.Entities;

namespace BusinessManagementAPI.UseCases.TransactionAllocations.GetTransactionAllocationsByTransaction;


public class GetAllocationsByTransactionResponse
{
    public List<TransactionAllocation> TransactionAllocations { get; set; }
}
