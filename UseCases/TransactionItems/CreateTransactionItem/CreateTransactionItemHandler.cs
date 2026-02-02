using AutoMapper;
using BusinessManagementAPI.Models;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.TransactionItems.CreateTransactionItem;

public class CreateTransactionItemHandler(IMapper mapper) : BaseHandler(mapper)
{
    public async Task<CreateTransactionItemResponse> CreateTransactionItem(CreateTransactionItemRequest request)
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

        return m_Mapper.Map<CreateTransactionItemResponse>(_TransactionItem);
    }
}
