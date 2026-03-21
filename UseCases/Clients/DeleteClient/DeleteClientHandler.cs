using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Base;

namespace MacsBusinessManagementAPI.UseCases.Clients.DeleteClient
{

    public class DeleteClientHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<DeleteClientRequest>
    {
        public async Task<IResult> HandleAsync(DeleteClientRequest request, CancellationToken cancellationToken)
        {
            var _Client = m_Context.GetEntities<Client>()
                .SingleOrDefault(c => c.ClientID == request.ClientID);

            if (_Client is null)
                return Results.NotFound("Client not found.");

            var _Invoices = m_Context.Invoices.Where(i => i.ClientID == request.ClientID).ToList();
            var _Receipts = m_Context.Receipts.Where(i => i.ClientID == request.ClientID).ToList();

            if (_Invoices.Count > 0 || _Receipts.Count > 0)
                return Results.Conflict($"{_Client.ClientName} has Transactions in the system and cannot be deleted.");

            m_Context.Clients.Remove(_Client);

            await m_Context.SaveChangesAsync(cancellationToken);

            return Results.NoContent();
        }
    }

}
