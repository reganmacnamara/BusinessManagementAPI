using AutoMapper;
using InvoiceAutomationAPI.Data;
using InvoiceAutomationAPI.Models;

namespace InvoiceAutomationAPI.UseCases.Clients.CreateClient
{

    public class CreateClientHandler
    {
        SQLContext m_Context = new();
        IMapper m_Mapper = default!;

        public CreateClientHandler(IMapper mapper)
        {
            m_Mapper = mapper;
        }

        public async Task<CreateClientResponse> CreateClient(CreateClientRequest request)
        {
            Client client = new()
            {
                ClientName = request.ClientName
            };

            m_Context.Clients.Add(client);

            await m_Context.SaveChangesAsync();

            var _Response = m_Mapper.Map<CreateClientResponse>(client);

            return _Response;
        }
    }

}
