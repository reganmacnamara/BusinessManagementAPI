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
            [FromServices] IUseCaseHandler<CreateClientRequest> handler,
            CancellationToken cancellationToken)
        {
            var _Response = await handler.HandleAsync(request, cancellationToken);

            return _Response;
        }

        [HttpDelete("{clientID}")]
        public async Task<IResult> DeleteClient([FromRoute] long clientID,
            [FromServices] PipelineMediator<DeleteClientRequest> mediator,
            CancellationToken cancellationToken)
            => await mediator.InvokeUseCaseAsync(new() { ClientID = clientID }, cancellationToken);

        [HttpGet("{clientID}")]
        public async Task<IResult> GetClient([FromRoute] long clientID,
            [FromServices] IUseCaseHandler<GetClientRequest> handler,
            CancellationToken cancellationToken)
        {
            var _Result = await handler.HandleAsync(new() { ClientId = clientID }, cancellationToken);

            return _Result;
        }

        [HttpGet]
        public async Task<IResult> GetClients([FromServices] IUseCaseHandler<GetClientsRequest> handler,
            CancellationToken cancellationToken)
        {
            var _Result = await handler.HandleAsync(new(), cancellationToken);

            return _Result;
        }

        [HttpPatch]
        public async Task<IResult> UpdateClient([FromBody] UpdateClientRequest request,
            [FromServices] IUseCaseHandler<UpdateClientRequest> handler,
            CancellationToken cancellationToken)
        {
            var _Result = await handler.HandleAsync(request, cancellationToken);

            return _Result;
        }
    }
}
