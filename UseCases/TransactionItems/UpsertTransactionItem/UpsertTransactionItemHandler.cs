using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Models;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.TransactionItems.UpsertTransactionItem;

public class UpsertTransactionItemHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<UpsertTransactionItemResponse> CreateTransactionItem(UpsertTransactionItemRequest request)
    {
        var _Transaction = m_Context.Transactions.Where(transaction => transaction.TransactionID == request.TransactionID).SingleOrDefault();
        var _Product = m_Context.Products.Where(product => product.ProductID == request.ProductID).SingleOrDefault();

        var _TransactionItem = m_Mapper.Map<TransactionItem>(request);

        if (_Transaction is not null)
            _TransactionItem.Transaction = _Transaction;
        else
            throw new Exception("Transaction not Found.");

        if (_Product is not null)
            _TransactionItem.Product = _Product;
        else
            throw new Exception("Product not Found.");

        m_Context.TransactionItems.Add(_TransactionItem);

        await m_Context.SaveChangesAsync();

        return m_Mapper.Map<UpsertTransactionItemResponse>(_TransactionItem);
    }

    public async Task<UpsertTransactionItemResponse> UpdateTransactionItem(UpsertTransactionItemRequest request)
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
                    p.Name != "TransactionItemID");

                if (targetProperty != null)
                {
                    var value = property.GetValue(request, null);
                    targetProperty.SetValue(_TransactionItem, value, null);
                }
            }

            await m_Context.SaveChangesAsync();

            var _Response = new UpsertTransactionItemResponse()
            {
                TransactionItemID = _TransactionItem.TransactionItemID
            };

            return _Response;
        }
        else
            throw new Exception("TransactionItem not found.");
    }
}
