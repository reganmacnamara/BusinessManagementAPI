using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Products.GetProducts;

public class GetProductsHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> GetProducts()
        => Results.Ok(new GetProductsResponse { Products = m_Context.Products.ToList() });
}
