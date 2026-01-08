using AutoMapper;
using InvoiceAutomationAPI.UseCases.Base;

namespace InvoiceAutomationAPI.UseCases.Clients.GetClients
{

    public class GetClientsHandler(IMapper mapper) : BaseHandler(mapper)
    {
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
