using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Transactions.UpdateTransaction;

public class UpdateTransactionHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{

    public async Task<UpdateTransactionResponse> UpdateTransaction(UpdateTransactionRequest request)
    {
        var _Transaction = await m_Context.Transactions.FindAsync(request.TransactionID);

        if (_Transaction is not null)
        {
            var _Client = await m_Context.Clients.FindAsync(request.ClientID);

            if (_Client is null)
                throw new Exception("Client not found.");

            _Transaction = UpdateEntityFromRequest(_Transaction, request, ["TransactionID"]);

            _Transaction.Client = _Client;

            await m_Context.SaveChangesAsync();

            var _Response = new UpdateTransactionResponse()
            {
                TransactionID = _Transaction.TransactionID
            };

            return _Response;
        }
        else
            throw new Exception("Transaction not found.");
    }

}
