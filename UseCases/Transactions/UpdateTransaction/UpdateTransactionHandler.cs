using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Transactions.UpdateTransaction;

public class UpdateTransactionHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{

    public async Task<UpdateTransactionResponse> UpdateTransaction(UpdateTransactionRequest request)
    {
        var _Transaction = await m_Context.Transactions.FindAsync(request.TransactionId);

        if (_Transaction is not null)
        {
            var _RequestProperties = request.GetType().GetProperties();
            var _TransactionProperties = _Transaction.GetType().GetProperties();

            foreach (var property in _RequestProperties)
            {
                var targetProperty = _TransactionProperties.FirstOrDefault(p =>
                    p.Name == property.Name &&
                    p.PropertyType == property.PropertyType &&
                    p.CanWrite &&
                    p.Name != "TransactionId");

                if (targetProperty != null)
                {
                    var value = property.GetValue(request, null);
                    targetProperty.SetValue(_Transaction, value, null);
                }
            }

            await m_Context.SaveChangesAsync();

            var _Response = new UpdateTransactionResponse()
            {
                TransactionId = _Transaction.TransactionID
            };

            return _Response;
        }
        else
            throw new Exception("Transaction not found.");
    }

}
