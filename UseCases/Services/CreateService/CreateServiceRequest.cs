using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Services.CreateService;

public class CreateServiceRequest : IUseCaseRequest
{
    public required string ServiceName { get; set; }

    public string ServiceDescription { get; set; } = string.Empty;

    public decimal EstimatedDuration { get; set; }

    public DurationUnit DurationUnit { get; set; }

    public decimal UnitCost { get; set; }

    public decimal UnitPrice { get; set; }
}
