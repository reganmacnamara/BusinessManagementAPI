using AutoMapper;
using InvoiceAutomationAPI.Data;

namespace InvoiceAutomationAPI.UseCases.Clients.DeleteClient
{

    public class DeleteClientHandler
    {
        SQLContext m_Context = new();
        IMapper m_Mapper = default!;

        public DeleteClientHandler(IMapper mapper)
        {
            m_Mapper = mapper;
        }

        public async Task DeleteClient(DeleteClientRequest request)
        {
            var _Client = m_Context.Clients.Where(client => client.ClientID == request.ClientID).SingleOrDefault();

            if (_Client is not null)
                m_Context.Clients.Remove(_Client);

            await m_Context.SaveChangesAsync();
        }
    }

}
