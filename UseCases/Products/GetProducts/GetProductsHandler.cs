using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Products.GetProducts;

public class GetProductsHandler(SQLContext context) : IUseCaseHandler<GetProductsRequest>
{
    public async Task<IResult> HandleAsync(GetProductsRequest request, CancellationToken cancellationToken)
    {
        var _Products = await context.GetEntities<Product>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var _Response = new GetProductsResponse
        {
            Products = _Products
        };

        return Results.Ok(_Response);
    }
}
