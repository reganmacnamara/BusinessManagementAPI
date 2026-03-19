using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.UseCases.Base;

namespace MacsBusinessManagementAPI.UseCases.Receipts.CreateReceipt;

public class CreateReceiptHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> CreateReceipt(CreateReceiptRequest request)
    {
        var _Client = m_Context.GetEntities<Client>()
            .SingleOrDefault(c => c.ClientID == request.ClientID);

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
