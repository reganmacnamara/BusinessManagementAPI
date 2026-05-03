using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using MacsBusinessManagementAPI.UseCases.Services.CreateService;
using MacsBusinessManagementAPI.UseCases.Services.DeleteService;
using MacsBusinessManagementAPI.UseCases.Services.DeleteServiceActivity;
using MacsBusinessManagementAPI.UseCases.Services.GetService;
using MacsBusinessManagementAPI.UseCases.Services.GetServices;
using MacsBusinessManagementAPI.UseCases.Services.UpdateService;
using MacsBusinessManagementAPI.UseCases.Services.UpsertServiceActivity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace MacsBusinessManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
[EnableRateLimiting("Authenticated")]
public class ServiceController : ControllerBase
{
    [HttpPost]
    public async Task<IResult> CreateService([FromBody] CreateServiceRequest request,
        [FromServices] UseCaseMediator<CreateServiceRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(request, cancellationToken);

    [HttpDelete("{serviceID}")]
    public async Task<IResult> DeleteService([FromRoute] long serviceID,
        [FromServices] UseCaseMediator<DeleteServiceRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(new() { ServiceID = serviceID }, cancellationToken);

    [HttpGet("{serviceID}")]
    public async Task<IResult> GetService([FromRoute] long serviceID,
        [FromServices] UseCaseMediator<GetServiceRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(new() { ServiceID = serviceID }, cancellationToken);

    [HttpGet]
    public async Task<IResult> GetServices(
        [FromServices] UseCaseMediator<GetServicesRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(new(), cancellationToken);

    [HttpPatch]
    public async Task<IResult> UpdateService([FromBody] UpdateServiceRequest request,
        [FromServices] UseCaseMediator<UpdateServiceRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(request, cancellationToken);

    [HttpPut("Activity")]
    public async Task<IResult> UpsertServiceActivity([FromBody] UpsertServiceActivityRequest request,
        [FromServices] UseCaseMediator<UpsertServiceActivityRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(request, cancellationToken);

    [HttpDelete("Activity/{serviceActivityID}")]
    public async Task<IResult> DeleteServiceActivity([FromRoute] long serviceActivityID,
        [FromServices] UseCaseMediator<DeleteServiceActivityRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(new() { ServiceActivityID = serviceActivityID }, cancellationToken);
}
