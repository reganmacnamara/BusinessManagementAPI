using AutoMapper;
using InvoiceAutomationAPI.UseCases.Clients.CreateClient;
using InvoiceAutomationAPI.UseCases.Clients.DeleteClient;
using InvoiceAutomationAPI.UseCases.Clients.GetClient;
using InvoiceAutomationAPI.UseCases.Clients.GetClients;
using InvoiceAutomationAPI.UseCases.Clients.UpdateClient;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceAutomationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController(IMapper mapper) : ControllerBase
    {
        IMapper m_Mapper = mapper;

        [HttpPost("Create")]
        public async Task<IResult> CreateClient([FromBody] CreateClientRequest request)
        {
            var handler = new CreateClientHandler(m_Mapper);

            var _Response = await handler.CreateClient(request);

            return _Response is CreateClientResponse
                ? Results.Ok(_Response)
                : Results.BadRequest();
        }

        [HttpPost("Delete")]
        public async Task<IResult> DeleteClient([FromBody] DeleteClientRequest request)
        {
            var handler = new DeleteClientHandler(m_Mapper);

            await handler.DeleteClient(request);

            return Results.NoContent();
        }

        [HttpGet("{clientID}")]
        public async Task<IResult> GetClient([FromRoute] long clientID)
        {
            var _Request = new GetClientRequest()
            {
                ClientId = clientID
            };

            var handler = new GetClientHandler(m_Mapper);

            var _Result = await handler.GetClient(_Request);

            return _Result is not null
                ? Results.Ok(_Result)
                : Results.NotFound();
        }

        [HttpGet]
        public async Task<IResult> GetClients()
        {
            var handler = new GetClientsHandler(m_Mapper);

            var _Result = await handler.GetClients();

            return Results.Ok(_Result);
        }

        [HttpPost("Update")]
        public async Task<IResult> UpdateClient([FromBody] UpdateClientRequest request)
        {
            var handler = new UpdateClientHandler(m_Mapper);

            var _Result = await handler.UpdateClient(request);

            return _Result.ClientID != 0
                ? Results.Ok(_Result)
                : Results.NotFound();
        }
    }
}
