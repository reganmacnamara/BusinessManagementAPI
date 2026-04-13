using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Clients.DeleteClient
{

    public class DeleteClientHandler(SQLContext context) : IUseCaseHandler<DeleteClientRequest>
    {
        public async Task<IResult> HandleAsync(DeleteClientRequest request, CancellationToken cancellationToken)
        {
            var _Client = await context.GetEntities<Client>()
                .SingleAsync(c => c.ClientID == request.ClientID, cancellationToken);

            var _Invoices = context.Invoices.Where(i => i.ClientID == request.ClientID).ToList();
            var _Receipts = context.Receipts.Where(i => i.ClientID == request.ClientID).ToList();

            if (_Invoices.Count > 0 || _Receipts.Count > 0)
                return Results.Conflict($"{_Client.ClientName} has Transactions in the system and cannot be deleted.");

            context.Clients.Remove(_Client);

            _ = await context.SaveChangesAsync(cancellationToken);

            return Results.NoContent();
        }
    }

}
