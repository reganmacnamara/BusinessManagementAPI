using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Invoices.GetInvoices;

public class GetInvoicesHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<GetInvoicesRequest>
{
    public async Task<IResult> HandleAsync(GetInvoicesRequest request, CancellationToken cancellationToken)
    {
        var _Response = new GetInvoicesResponse()
        {
            Invoices = [.. m_Context.GetEntities<Invoice>().AsNoTracking().Include(i => i.Client)]
        };

        return Results.Ok(_Response);
    }
}
