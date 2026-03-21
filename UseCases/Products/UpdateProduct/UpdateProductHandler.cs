using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Extensions;
using MacsBusinessManagementAPI.Infrastructure;

namespace MacsBusinessManagementAPI.UseCases.Products.UpdateProduct;

public class UpdateProductHandler(SQLContext context) : IUseCaseHandler<UpdateProductRequest>
{
    public async Task<IResult> HandleAsync(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var _Product = context.GetEntities<Product>()
            .SingleOrDefault(p => p.ProductID == request.ProductID);

        if (_Product is null)
            return Results.NotFound("Product not found.");

        _Product.UpdateFromEntity(request, ["ProductID"]);

        await context.SaveChangesAsync(cancellationToken);

        var _Response = new UpdateProductResponse()
        {
            ProductId = _Product.ProductID
        };

        return Results.Ok(_Response);
    }
}
