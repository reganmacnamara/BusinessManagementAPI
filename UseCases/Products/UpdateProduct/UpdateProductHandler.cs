using AutoMapper;
using MacsBusinessManagementAPI.UseCases.Products.UpdateProduct;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.UseCases.Base;

namespace MacsBusinessManagementAPI.UseCases.Products.UpdateProduct;

public class UpdateProductHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> UpdateProduct(UpdateProductRequest request)
    {
        var _Product = m_Context.GetEntities<Product>()
            .SingleOrDefault(p => p.ProductID == request.ProductID);

        if (_Product is null)
            return Results.NotFound("Product not found.");

        _Product = UpdateEntityFromRequest(_Product, request, ["ProductID"]);

        await m_Context.SaveChangesAsync();

        var _Response = new UpdateProductResponse()
        {
            ProductId = _Product.ProductID
        };

        return Results.Ok(_Response);
    }
}
