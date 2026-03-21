using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Invoices.GetClientInvoices;

public class GetClientInvoicesHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<GetClientInvoicesRequest>
{
    public async Task<IResult> HandleAsync(GetClientInvoicesRequest request, CancellationToken cancellationToken)
    {
        var _Invoices = m_Context.GetEntities<Invoice>()
            .AsNoTracking()
            .Include(i => i.Client)
            .Where(i => i.ClientID == request.ClientID)
            .ToList();

        var _Response = new GetClientInvoicesResponse()
        {
            Invoices = _Invoices
        };

        return Results.Ok(_Response);
    }
}
