using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Models;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Products.CreateProduct;

public class CreateProductHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<CreateProductResponse> CreateProduct(CreateProductRequest request)
    {
        var _Product = m_Mapper.Map<Product>(request);

        m_Context.Products.Add(_Product);

        await m_Context.SaveChangesAsync();

        return new CreateProductResponse() { ProductID = _Product.ProductID };
    }
}
