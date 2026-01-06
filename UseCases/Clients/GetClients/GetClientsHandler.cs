using AutoMapper;
using InvoiceAutomationAPI.Data;

namespace InvoiceAutomationAPI.UseCases.Clients.GetClients
{

    public class GetClientsHandler
    {
        SQLContext m_Context = new();
        IMapper m_Mapper = default!;

        public GetClientsHandler(IMapper mapper)
        {
            m_Mapper = mapper;
        }

        public async Task<GetClientsResponse> GetClients()
        {
            var _Clients = m_Context.Clients.ToList();

            var _Response = _Clients.Count != 0
                ? m_Mapper.Map<GetClientsResponse>(_Clients)
                : new GetClientsResponse();

            return _Response;
        }
    }

}
