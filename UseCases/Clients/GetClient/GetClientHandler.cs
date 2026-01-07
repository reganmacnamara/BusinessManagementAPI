using AutoMapper;
using InvoiceAutomationAPI.Data;

namespace InvoiceAutomationAPI.UseCases.Clients.GetClient
{

    public class GetClientHandler
    {
        SQLContext m_Context = new();
        IMapper m_Mapper = default!;

        public GetClientHandler(IMapper mapper)
            => m_Mapper = mapper;

        public async Task<GetClientResponse> GetClient(GetClientRequest request)
        {
            var _Client = m_Context.Clients.Find(request.ClientId);

            var _Response = m_Mapper.Map<GetClientResponse>(_Client);

            return _Response;
        }
    }

}
