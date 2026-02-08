using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Transactions.UpdateTransaction;

public class UpdateTransactionHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{

    public async Task<IResult> UpdateTransaction(UpdateTransactionRequest request)
    {
        var _Transaction = m_Context.Transactions.Find(request.TransactionID);

        if (_Transaction is not null)
        {
            var _Client = m_Context.Clients.Find(request.ClientID);

            if (_Client is null)
                return Results.NotFound("Client not found.");

            _Transaction = UpdateEntityFromRequest(_Transaction, request, ["TransactionID"]);

            _Transaction.Client = _Client;

            await m_Context.SaveChangesAsync();

            var _Response = new UpdateTransactionResponse()
            {
                TransactionID = _Transaction.TransactionID
            };

            return Results.Ok(_Response);
        }
        else
            return Results.NotFound("Transaction not found.");
    }

}
