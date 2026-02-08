using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Clients.DeleteClient
{

    public class DeleteClientHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
    {
        public async Task<IResult> DeleteClient(DeleteClientRequest request)
        {
            var _Client = m_Context.Clients.Find(request.ClientID);

            if (_Client is not null)
            {
                var _ClientTransactions = m_Context.Transactions.Where(t => t.ClientID == _Client.ClientID);

                if (_ClientTransactions.Count() > 0)
                    throw new Exception($"{_Client.ClientName} has Transactions in the system and cannot be deleted.");

                m_Context.Clients.Remove(_Client);
                await m_Context.SaveChangesAsync();

                return Results.NoContent();
            }
            else
                return Results.NotFound("Client not found.");
        }
    }

}
