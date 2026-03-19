using AutoMapper;
using MacsBusinessManagementAPI.UseCases.Invoices.GetInvoices;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Invoices.GetInvoices;

public class GetInvoicesHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> GetInvoices()
    {
        var _Response = new GetInvoicesResponse()
        {
            Invoices = [.. m_Context.GetEntities<Invoice>().AsNoTracking().Include(i => i.Client)]
        };

        return Results.Ok(_Response);
    }
}
