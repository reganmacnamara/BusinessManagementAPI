using AutoMapper;
using InvoiceAutomationAPI.UseCases.Clients.CreateClient;
using InvoiceAutomationAPI.UseCases.Clients.DeleteClient;
using InvoiceAutomationAPI.UseCases.Clients.GetClient;
using InvoiceAutomationAPI.UseCases.Clients.GetClients;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceAutomationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController(IMapper mapper) : ControllerBase
    {
        IMapper m_Mapper = mapper;

        [HttpPost("Create")]
        public async Task<IResult> CreateClient([FromForm] string clientName,
            [FromForm] string clientEmail,
            [FromForm] string clientPhone,
            [FromForm] string clientMobile,
            [FromForm] string addressLine1,
            [FromForm] string addressLine2,
            [FromForm] string postCode,
            [FromForm] string state,
            [FromForm] string country)
        {
            var handler = new CreateClientHandler(m_Mapper);

            var _Request = new CreateClientRequest
            {
                ClientName = clientName,
                ClientEmail = clientEmail,
                ClientPhone = clientPhone,
                ClientMobile = clientMobile,
                AddressLine1 = addressLine1,
                AddressLine2 = addressLine2,
                PostCode = postCode,
                State = state,
                Country = country
            };

            var _Response = await handler.CreateClient(_Request);

            return _Response is CreateClientResponse
                ? Results.Ok(_Response)
                : Results.BadRequest();
        }

        [HttpDelete("Delete")]
        public async Task<IResult> DeleteClient([FromForm] long clientID)
        {
            var handler = new DeleteClientHandler(m_Mapper);

            var _Request = new DeleteClientRequest
            {
                ClientID = clientID
            };

            await handler.DeleteClient(_Request);

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

            return _Result.Clients.Count != 0
                ? Results.Ok(_Result)
                : Results.NotFound();
        }
    }
}
