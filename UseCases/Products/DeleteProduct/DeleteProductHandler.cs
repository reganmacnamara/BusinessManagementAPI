using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Products.DeleteProduct;

public class DeleteProductHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> DeleteProduct(DeleteProductRequest request)
    {
        var _Product = m_Context.GetEntities<Product>()
            .Where(product => product.ProductID == request.ProductID)
            .Include(p => p.InvoiceItems)
            .SingleOrDefault();

        if (_Product is not null)
        {
            var _IsProductUsed = _Product.InvoiceItems.Count != 0;

            if (_IsProductUsed)
                return Results.Conflict("Cannot delete a Product that still has history.");

            m_Context.Products.Remove(_Product);
            await m_Context.SaveChangesAsync();
            return Results.NoContent();
        }
        else
            return Results.NotFound("Product not found.");
    }
}
