using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Clients.DeleteClient
{

    public class DeleteClientHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
    {
        public async Task DeleteClient(DeleteClientRequest request)
        {
            var _Client = m_Context.Clients.Find(request.ClientID);

            if (_Client is not null)
            {
                m_Context.Clients.Remove(_Client);
                await m_Context.SaveChangesAsync();
            }
            else
                throw new Exception("Client not found.");
        }
    }

}
