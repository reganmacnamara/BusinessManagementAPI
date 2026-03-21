using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Base;

namespace MacsBusinessManagementAPI.UseCases.Products.CreateProduct;

public class CreateProductHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<CreateProductRequest>
{
    public async Task<IResult> HandleAsync(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var _Product = m_Mapper.Map<Product>(request);

        m_Context.Products.Add(_Product);

        await m_Context.SaveChangesAsync(cancellationToken);

        return Results.Created(string.Empty, new CreateProductResponse() { ProductID = _Product.ProductID });
    }
}
