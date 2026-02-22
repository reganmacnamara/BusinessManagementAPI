using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Clients.DeleteClient
{

    public class DeleteClientHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
    {
        public async Task<IResult> DeleteClient(DeleteClientRequest request)
        {
            var _Client = m_Context.GetEntities<Client>()
                .Where(c => c.ClientID == request.ClientID)
                .Include(c => c.Invoices)
                .Include(c => c.Receipts)
                .SingleOrDefault();

            if (_Client is not null)
            {
                if (_Client.Invoices.Count > 0 || _Client.Receipts.Count > 0)
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
