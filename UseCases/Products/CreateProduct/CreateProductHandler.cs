using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;

namespace MacsBusinessManagementAPI.UseCases.Products.CreateProduct;

public class CreateProductHandler(IMapper mapper, SQLContext context) : IUseCaseHandler<CreateProductRequest>
{
    public async Task<IResult> HandleAsync(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var _Product = mapper.Map<Product>(request);

        context.Products.Add(_Product);

        _ = await context.SaveChangesAsync(cancellationToken);

        return Results.Created(string.Empty, new CreateProductResponse() { ProductID = _Product.ProductID });
    }
}
