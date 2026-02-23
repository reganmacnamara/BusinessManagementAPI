using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Invoices.GetInvoices;

public class GetInvoicesHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> GetInvoices()
    {
        var _Response = new GetInvoicesResponse()
        {
            Invoices = [.. m_Context.Invoices.Include(i => i.Client)]
        };

        return Results.Ok(_Response);
    }
}
