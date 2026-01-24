using AutoMapper;
using InvoiceAutomationAPI.UseCases.Base;

namespace InvoiceAutomationAPI.UseCases.TransactionItems.UpdateTransactionItem;

public class UpdateTransactionItemHandler(IMapper mapper) : BaseHandler(mapper)
{
    public async Task<UpdateTransactionItemResponse> UpdateTransactionItem(UpdateTransactionItemRequest request)
    {
        var _TransactionItem = await m_Context.TransactionItems.FindAsync(request.TransactionItemID);

        if (_TransactionItem is not null)
        {
            var _Product = m_Context.Products.Find(request.ProductID);

            if (_Product is not null)
                _TransactionItem.Product = _Product;
            else
                throw new Exception("Product not found.");

            var _RequestProperties = request.GetType().GetProperties();
            var _TransactionProperties = _TransactionItem.GetType().GetProperties();

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
                    targetProperty.SetValue(_TransactionItem, value, null);
                }
            }

            await m_Context.SaveChangesAsync();

            var _Response = new UpdateTransactionItemResponse()
            {
                TransactionItemID = _TransactionItem.TransactionItemID
            };

            return _Response;
        }
        else
            throw new Exception("TransactionItem not found.");
    }
}
