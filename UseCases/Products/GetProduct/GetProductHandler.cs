using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Products.GetProduct;

public class GetProductHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<GetProductResponse> GetProduct(GetProductRequest request)
    {
        var _Product = await m_Context.Products.Where(product => product.ProductID == request.ProductID).SingleAsync();

        var _Response = m_Mapper.Map<GetProductResponse>(_Product);

        return _Response;
    }
}
