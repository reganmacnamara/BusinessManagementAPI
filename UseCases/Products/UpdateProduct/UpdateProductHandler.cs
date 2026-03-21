using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Base;

namespace MacsBusinessManagementAPI.UseCases.Products.UpdateProduct;

public class UpdateProductHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<UpdateProductRequest>
{
    public async Task<IResult> HandleAsync(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var _Product = m_Context.GetEntities<Product>()
            .SingleOrDefault(p => p.ProductID == request.ProductID);

        if (_Product is null)
            return Results.NotFound("Product not found.");

        _Product = UpdateEntityFromRequest(_Product, request, ["ProductID"]);

        await m_Context.SaveChangesAsync(cancellationToken);

        var _Response = new UpdateProductResponse()
        {
            ProductId = _Product.ProductID
        };

        return Results.Ok(_Response);
    }
}
