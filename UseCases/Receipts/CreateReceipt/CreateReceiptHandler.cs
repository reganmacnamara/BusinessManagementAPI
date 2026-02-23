using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Receipts.CreateReceipt;

public class CreateReceiptHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> CreateReceipt(CreateReceiptRequest request)
    {
        var _Client = m_Context.GetEntities<Client>()
            .Where(c => c.ClientID == request.ClientID)
            .SingleOrDefault();

        if (_Client is null)
            return Results.NotFound("Client not found.");

        var _Receipt = m_Mapper.Map<Receipt>(request);

        _Receipt.Client = _Client;
        _Receipt.Outstanding = true;

        m_Context.Receipts.Add(_Receipt);

        _ = await m_Context.SaveChangesAsync();

        var _Response = m_Mapper.Map<CreateReceiptResponse>(_Receipt);

        return Results.Created(string.Empty, _Response);
    }
}
