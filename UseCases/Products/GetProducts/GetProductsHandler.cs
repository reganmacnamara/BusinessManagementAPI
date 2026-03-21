using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Products.GetProducts;

public class GetProductsHandler(SQLContext context) : IUseCaseHandler<GetProductsRequest>
{
    public async Task<IResult> HandleAsync(GetProductsRequest request, CancellationToken cancellationToken)
        => Results.Ok(new GetProductsResponse { Products = [.. context.GetEntities<Product>().AsNoTracking()] });
}
