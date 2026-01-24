using AutoMapper;
using InvoiceAutomationAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace InvoiceAutomationAPI.UseCases.Products.GetProduct;

public class GetProductHandler(IMapper mapper) : BaseHandler(mapper)
{
    public async Task<GetProductResponse> GetProduct(GetProductRequest request)
    {
        var _Product = await m_Context.Products.Where(product => product.ProductID == request.ProductID).SingleAsync();

        var _Response = m_Mapper.Map<GetProductResponse>(_Product);

        return _Response;
    }
}
