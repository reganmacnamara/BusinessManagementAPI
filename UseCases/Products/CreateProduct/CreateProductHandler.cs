using AutoMapper;
using InvoiceAutomationAPI.Models;
using InvoiceAutomationAPI.UseCases.Base;

namespace InvoiceAutomationAPI.UseCases.Products.CreateProduct;

public class CreateProductHandler(IMapper mapper) : BaseHandler(mapper)
{
    public async Task<CreateProductResponse> CreateProduct(CreateProductRequest request)
    {
        var _Product = m_Mapper.Map<Product>(request);

        m_Context.Products.Add(_Product);

        await m_Context.SaveChangesAsync();

        return new CreateProductResponse() { ProductID = _Product.ProductID };
    }
}
