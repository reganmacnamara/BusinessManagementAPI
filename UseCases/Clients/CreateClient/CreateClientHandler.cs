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
            var _Client = m_Mapper.Map<Client>(request);

            m_Context.Clients.Add(_Client);

            await m_Context.SaveChangesAsync();

            var _Response = m_Mapper.Map<CreateClientResponse>(_Client);

            return _Response;
        }
    }

}
