using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Products.UpdateProduct;

public class UpdateProductHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request)
    {
        var _Product = await m_Context.Products.FindAsync(request.ProductID);

        if (_Product is not null)
        {
            _Product = UpdateEntityFromRequest(_Product, request, ["ProductID"]);

            await m_Context.SaveChangesAsync();

            var _Response = new UpdateProductResponse()
            {
                ProductId = _Product.ProductID
            };

            return _Response;
        }

        return new UpdateProductResponse();
    }
}
