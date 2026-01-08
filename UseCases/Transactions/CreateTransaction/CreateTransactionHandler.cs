using AutoMapper;
using InvoiceAutomationAPI.Models;
using InvoiceAutomationAPI.UseCases.Base;

namespace InvoiceAutomationAPI.UseCases.Transactions.CreateTransaction
{

    public class CreateTransactionHandler(IMapper mapper) : BaseHandler(mapper)
    {
        public async Task<CreateTransactionResponse> CreateTransaction(CreateTransactionRequest request)
        {
            var _Transaction = m_Mapper.Map<Transaction>(request);

            m_Context.Transactions.Add(_Transaction);

            await m_Context.SaveChangesAsync();

            var _Response = m_Mapper.Map<CreateTransactionResponse>(_Transaction);

            return _Response;
        }
    }

}
