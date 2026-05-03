using System.ComponentModel.DataAnnotations;

namespace MacsBusinessManagementAPI.Entities;

public class ServiceActivity
{
    [Key]
    public long ServiceActivityID { get; set; }

    public long ServiceID { get; set; }

    public required string ActivityName { get; set; }

    public string Description { get; set; } = string.Empty;

    public int SortOrder { get; set; }

    //Navigation Properties

    public Service Service { get; set; } = default!;
}
