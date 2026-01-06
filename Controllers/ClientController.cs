using AutoMapper;
using InvoiceAutomationAPI.UseCases.Clients.CreateClient;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceAutomationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController(IMapper mapper) : ControllerBase
    {
        IMapper m_Mapper = mapper;

        [HttpPost("Create")]
        public async Task<CreateClientResponse> CreateClient([FromForm] string clientName)
        {
            var handler = new CreateClientHandler(m_Mapper);

            var _Request = new CreateClientRequest
            {
                ClientName = clientName
            };

            var _Response = await handler.CreateClient(_Request);

            return _Response;
        }
    }
}
