using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Products.DeleteProduct;

public class DeleteProductHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> DeleteProduct(DeleteProductRequest request)
    {
        var _Product = m_Context.Products.FirstOrDefault(product => product.ProductID == request.ProductID);

        if (_Product is not null)
        {
            m_Context.Products.Remove(_Product);
            await m_Context.SaveChangesAsync();
            return Results.NoContent();
        }
        else
            return Results.NotFound("Product not found.");
    }
}
