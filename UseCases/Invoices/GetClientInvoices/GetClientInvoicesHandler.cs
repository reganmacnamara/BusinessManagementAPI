using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Invoices.GetClientInvoices;

public class GetClientInvoicesHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> GetClientInvoices(GetClientInvoicesRequest request)
    {
        var _Invoices = m_Context.GetEntities<Invoice>()
            .Where(i => i.ClientID == request.ClientID)
            .ToList();

        var _Response = new GetClientInvoicesResponse()
        {
            Invoices = _Invoices
        };

        return Results.Ok(_Response);
    }
}
