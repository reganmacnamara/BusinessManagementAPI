using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Transactions.DeleteTransaction
{

    public class DeleteTransactionHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
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
