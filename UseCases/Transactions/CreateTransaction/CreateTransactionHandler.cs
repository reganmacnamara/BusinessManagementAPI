using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Transactions.CreateTransaction
{

    public class CreateTransactionHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
    {
        public async Task<IResult> CreateTransaction(CreateTransactionRequest request)
        {
            var _Client = m_Context.Clients.Find(request.ClientID);

            var _Transaction = m_Mapper.Map<Transaction>(request);

            if (_Client is not null)
                _Transaction.Client = _Client;
            else
                return Results.NotFound("Client not found.");

            if (_Transaction.TransactionType == "REC")
                _Transaction.DueDate = null;

            m_Context.Transactions.Add(_Transaction);

            await m_Context.SaveChangesAsync();

            var _Response = m_Mapper.Map<CreateTransactionResponse>(_Transaction);

            return Results.Ok(_Response);
        }
    }

}
