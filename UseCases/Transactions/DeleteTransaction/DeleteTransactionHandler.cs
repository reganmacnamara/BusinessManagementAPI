using AutoMapper;
using InvoiceAutomationAPI.UseCases.Base;

namespace InvoiceAutomationAPI.UseCases.Transactions.DeleteTransaction
{

    public class DeleteTransactionHandler(IMapper mapper) : BaseHandler(mapper)
    {
        public async Task DeleteTransaction(DeleteTransactionRequest request)
        {
            var _Transaction = m_Context.Transactions.Find(request.TransactionID);

            if (_Transaction is not null)
            {
                m_Context.Transactions.Remove(_Transaction);
                await m_Context.SaveChangesAsync();
            }
            else
                throw new Exception("Transaction not found.");
        }
    }

}
