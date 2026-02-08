using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Products.UpdateProduct;

public class UpdateProductHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> UpdateProduct(UpdateProductRequest request)
    {
        var _Product = m_Context.Products.Find(request.ProductID);

        if (_Product is not null)
        {
            _Product = UpdateEntityFromRequest(_Product, request, ["ProductID"]);

            await m_Context.SaveChangesAsync();

            var _Response = new UpdateProductResponse()
            {
                ProductId = _Product.ProductID
            };

            return Results.Ok(_Response);
        }

        return Results.NotFound("Product not found.");
    }
}
