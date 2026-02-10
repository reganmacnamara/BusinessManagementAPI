using System.ComponentModel.DataAnnotations;

namespace BusinessManagementAPI.Entities;

public class TransactionAllocation
{
    [Key]
    public long TransactionAllocationID { get; set; }

    public long AllocatingID { get; set; }

    public long RecievingID { get; set; }

    public decimal AllocationValue { get; set; }

    public DateTime AllocationDate { get; set; }
}
