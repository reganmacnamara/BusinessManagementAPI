using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Products.GetProduct;

public class GetProductHandler(IMapper mapper, SQLContext context) : IUseCaseHandler<GetProductRequest>
{
    public async Task<IResult> HandleAsync(GetProductRequest request, CancellationToken cancellationToken)
    {
        var _Product = context.GetEntities<Product>()
            .AsNoTracking()
            .SingleOrDefault(p => p.ProductID == request.ProductID);

        if (_Product is null)
            return Results.NotFound("Product was not found.");

        var _Response = mapper.Map<GetProductResponse>(_Product);

        return Results.Ok(_Response);
    }
}
