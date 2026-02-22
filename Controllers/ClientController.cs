using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Clients.CreateClient;
using BusinessManagementAPI.UseCases.Clients.DeleteClient;
using BusinessManagementAPI.UseCases.Clients.GetClient;
using BusinessManagementAPI.UseCases.Clients.GetClients;
using BusinessManagementAPI.UseCases.Clients.UpdateClient;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController(IMapper mapper, SQLContext context) : ControllerBase
    {
        [HttpPost]
        public async Task<IResult> CreateClient([FromBody] CreateClientRequest request)
        {
            var handler = new CreateClientHandler(mapper, context);

            var _Response = await handler.CreateClient(request);

            return _Response;
        }

        [HttpDelete("{clientID}")]
        public async Task<IResult> DeleteClient([FromRoute] long clientID)
        {
            var _Request = new DeleteClientRequest()
            {
                ClientID = clientID
            };

            var handler = new DeleteClientHandler(mapper, context);

            var _Response = await handler.DeleteClient(_Request);

            return _Response;
        }

        [HttpGet("{clientID}")]
        public async Task<IResult> GetClient([FromRoute] long clientID)
        {
            var _Request = new GetClientRequest()
            {
                ClientId = clientID
            };

            var handler = new GetClientHandler(mapper, context);

            var _Result = await handler.GetClient(_Request);

            return _Result;
        }

        [HttpGet]
        public async Task<IResult> GetClients()
        {
            var handler = new GetClientsHandler(mapper, context);

            var _Result = await handler.GetClients();

            return _Result;
        }

        [HttpPatch]
        public async Task<IResult> UpdateClient([FromBody] UpdateClientRequest request)
        {
            var handler = new UpdateClientHandler(mapper, context);

            var _Result = await handler.UpdateClient(request);

            return _Result;
        }
    }
}
