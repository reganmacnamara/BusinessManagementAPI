using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Products.GetProduct;

public class GetProductHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> GetProduct(GetProductRequest request)
    {
        var _Product = m_Context.Find<Product>(request.ProductID);

        if (_Product is null)
            return Results.NotFound("Product was not found.");

        var _Response = m_Mapper.Map<GetProductResponse>(_Product);

        return Results.Ok(_Response);
    }
}
