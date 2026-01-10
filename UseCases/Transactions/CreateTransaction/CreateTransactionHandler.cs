using AutoMapper;
using InvoiceAutomationAPI.Models;
using InvoiceAutomationAPI.UseCases.Base;

namespace InvoiceAutomationAPI.UseCases.Transactions.CreateTransaction
{

    public class CreateTransactionHandler(IMapper mapper) : BaseHandler(mapper)
    {
        public async Task<CreateTransactionResponse> CreateTransaction(CreateTransactionRequest request)
        {
            var _Client = m_Context.Clients.Find(request.ClientID);

            var _Transaction = m_Mapper.Map<Transaction>(request);

            _Transaction.Client = _Client;

            if (_Transaction.TransactionType == "REC")
                _Transaction.DueDate = null;

            m_Context.Transactions.Add(_Transaction);

            await m_Context.SaveChangesAsync();

            var _Response = m_Mapper.Map<CreateTransactionResponse>(_Transaction);

            return _Response;
        }
    }

}
