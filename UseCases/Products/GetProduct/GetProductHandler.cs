using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Products.GetProduct;

public class GetProductHandler(IMapper mapper, SQLContext context) : IUseCaseHandler<GetProductRequest>
{
    public async Task<IResult> HandleAsync(GetProductRequest request, CancellationToken cancellationToken)
    {
        var _Product = await context.GetEntities<Product>()
            .AsNoTracking()
            .SingleAsync(p => p.ProductID == request.ProductID, cancellationToken);

        var _Response = mapper.Map<GetProductResponse>(_Product);

        return Results.Ok(_Response);
    }
}
