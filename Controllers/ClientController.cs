using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using MacsBusinessManagementAPI.UseCases.Clients.CreateClient;
using MacsBusinessManagementAPI.UseCases.Clients.DeleteClient;
using MacsBusinessManagementAPI.UseCases.Clients.GetClient;
using MacsBusinessManagementAPI.UseCases.Clients.GetClients;
using MacsBusinessManagementAPI.UseCases.Clients.UpdateClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace MacsBusinessManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [EnableRateLimiting("Authenticated")]
    public class ClientController : ControllerBase
    {
        [HttpPost]
        public async Task<IResult> CreateClient([FromBody] CreateClientRequest request,
            [FromServices] PipelineMediator<CreateClientRequest> mediator,
            CancellationToken cancellationToken)
            => await mediator.InvokeUseCaseAsync(request, cancellationToken);

        [HttpDelete("{clientID}")]
        public async Task<IResult> DeleteClient([FromRoute] long clientID,
            [FromServices] PipelineMediator<DeleteClientRequest> mediator,
            CancellationToken cancellationToken)
            => await mediator.InvokeUseCaseAsync(new() { ClientID = clientID }, cancellationToken);

        [HttpGet("{clientID}")]
        public async Task<IResult> GetClient([FromRoute] long clientID,
            [FromServices] PipelineMediator<GetClientRequest> mediator,
            CancellationToken cancellationToken)
            => await mediator.InvokeUseCaseAsync(new() { ClientId = clientID }, cancellationToken);

        [HttpGet]
        public async Task<IResult> GetClients(
            [FromServices] PipelineMediator<GetClientsRequest> mediator,
            CancellationToken cancellationToken)
            => await mediator.InvokeUseCaseAsync(new(), cancellationToken);

        [HttpPatch]
        public async Task<IResult> UpdateClient([FromBody] UpdateClientRequest request,
            [FromServices] PipelineMediator<UpdateClientRequest> mediator,
            CancellationToken cancellationToken)
            => await mediator.InvokeUseCaseAsync(request, cancellationToken);
    }
}
