using System.ComponentModel.DataAnnotations;

namespace MacsBusinessManagementAPI.Entities;

public class Service
{
    [Key]
    public long ServiceID { get; set; }

    public long CompanyID { get; set; }

    public required string ServiceName { get; set; }

    public string ServiceDescription { get; set; } = string.Empty;

    public decimal EstimatedDuration { get; set; }

    public DurationUnit DurationUnit { get; set; }

    public decimal UnitCost { get; set; }

    public decimal UnitPrice { get; set; }

    //Navigation Properties

    public Company Company { get; set; } = default!;

    public ICollection<ServiceActivity> ServiceActivities { get; set; } = [];
}
