using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Products.GetProducts;

public class GetProductsHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<GetProductsRequest>
{
    public async Task<IResult> HandleAsync(GetProductsRequest request, CancellationToken cancellationToken)
        => Results.Ok(new GetProductsResponse { Products = [.. m_Context.GetEntities<Product>().AsNoTracking()] });
}
