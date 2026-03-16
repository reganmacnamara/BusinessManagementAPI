using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Products.GetProducts;

public class GetProductsHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> GetProducts()
        => Results.Ok(new GetProductsResponse { Products = [.. m_Context.GetEntities<Product>().AsNoTracking()] });
}
