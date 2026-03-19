using AutoMapper;
using MacsBusinessManagementAPI.UseCases.Products.CreateProduct;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.UseCases.Base;

namespace MacsBusinessManagementAPI.UseCases.Products.CreateProduct;

public class CreateProductHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> CreateProduct(CreateProductRequest request)
    {
        var _Product = m_Mapper.Map<Product>(request);

        m_Context.Products.Add(_Product);

        await m_Context.SaveChangesAsync();

        return Results.Created(string.Empty, new CreateProductResponse() { ProductID = _Product.ProductID });
    }
}
