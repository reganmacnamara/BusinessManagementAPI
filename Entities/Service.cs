using System.ComponentModel.DataAnnotations;

namespace BusinessManagementAPI.Entities;

public class Service
{
    [Key]
    public long ServiceID { get; set; }

    public string ServiceName { get; set; } = string.Empty;

    public string ServiceDescription { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal ServiceGross { get; set; }

    public decimal ServiceTax { get; set; }

    public decimal ServiceNet { get; set; }
}
