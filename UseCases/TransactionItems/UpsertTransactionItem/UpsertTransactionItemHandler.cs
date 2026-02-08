using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.TransactionItems.UpsertTransactionItem;

public class UpsertTransactionItemHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> CreateTransactionItem(UpsertTransactionItemRequest request)
    {
        var _Transaction = m_Context.Transactions.Where(transaction => transaction.TransactionID == request.TransactionID).SingleOrDefault();
        var _Product = m_Context.Products.Where(product => product.ProductID == request.ProductID).SingleOrDefault();

        var _TransactionItem = m_Mapper.Map<TransactionItem>(request);

        if (_Transaction is not null)
            _TransactionItem.Transaction = _Transaction;
        else
            return Results.NotFound("Transaction not Found.");

        if (_Product is not null)
            _TransactionItem.Product = _Product;
        else
            return Results.NotFound("Product not Found.");

        m_Context.TransactionItems.Add(_TransactionItem);

        await m_Context.SaveChangesAsync();

        return Results.Ok(m_Mapper.Map<UpsertTransactionItemResponse>(_TransactionItem));
    }

    public async Task<IResult> UpdateTransactionItem(UpsertTransactionItemRequest request)
    {
        var _TransactionItem = m_Context.TransactionItems.Find(request.TransactionItemID);

        if (_TransactionItem is not null)
        {
            var _Product = m_Context.Products.Find(request.ProductID);

            if (_Product is not null)
                _TransactionItem.Product = _Product;
            else
                return Results.NotFound("Product not found.");

            _TransactionItem = UpdateEntityFromRequest(_TransactionItem, request, ["TransactionItemID"]);

            await m_Context.SaveChangesAsync();

            var _Response = new UpsertTransactionItemResponse()
            {
                TransactionItemID = _TransactionItem.TransactionItemID
            };

            return Results.Ok(_Response);
        }
        else
            return Results.NotFound("TransactionItem not found.");
    }
}
