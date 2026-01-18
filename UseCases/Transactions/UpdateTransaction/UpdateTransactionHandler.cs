using AutoMapper;
using InvoiceAutomationAPI.UseCases.Base;

namespace InvoiceAutomationAPI.UseCases.Transactions.UpdateTransaction;

public class UpdateTransactionHandler(IMapper mapper) : BaseHandler(mapper)
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
                    p.Name != "ClientID");

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

        return new UpdateTransactionResponse();
    }

}
