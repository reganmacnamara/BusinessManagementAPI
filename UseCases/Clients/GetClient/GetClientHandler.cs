using AutoMapper;
using InvoiceAutomationAPI.UseCases.Base;

namespace InvoiceAutomationAPI.UseCases.Clients.GetClient
{

    public class GetClientHandler(IMapper mapper) : BaseHandler(mapper)
    {
        public async Task<GetClientResponse> GetClient(GetClientRequest request)
        {
            var _Client = m_Context.Clients.Find(request.ClientId);

            var _Response = m_Mapper.Map<GetClientResponse>(_Client);

            return _Response;
        }
    }

}
