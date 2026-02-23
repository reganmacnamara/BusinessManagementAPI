using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Clients.DeleteClient
{

    public class DeleteClientHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
    {
        public async Task<IResult> DeleteClient(DeleteClientRequest request)
        {
            var _Client = m_Context.GetEntities<Client>()
                .Where(c => c.ClientID == request.ClientID)
                .SingleOrDefault();

            if (_Client is not null)
            {
                var _Invoices = m_Context.Invoices.Where(i => i.ClientID == request.ClientID).ToList();
                var _Receipts = m_Context.Receipts.Where(i => i.ClientID == request.ClientID).ToList();

                if (_Invoices.Count > 0 || _Receipts.Count > 0)
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
