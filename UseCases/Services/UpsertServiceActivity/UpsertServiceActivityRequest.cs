using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Services.UpsertServiceActivity;

public class UpsertServiceActivityRequest : IUseCaseRequest
{
    public long ServiceActivityID { get; set; }
    public long ServiceID { get; set; }
    public string ActivityName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}
