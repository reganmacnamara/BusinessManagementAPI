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
            var _RequestProperties = request.GetType().GetProperties();
            var _ClientProperties = _Product.GetType().GetProperties();

            foreach (var property in _RequestProperties)
            {
                var targetProperty = _ClientProperties.FirstOrDefault(p =>
                    p.Name == property.Name &&
                    p.PropertyType == property.PropertyType &&
                    p.CanWrite &&
                    p.Name != "ProductID");

                if (targetProperty != null)
                {
                    var value = property.GetValue(request, null);
                    targetProperty.SetValue(_Product, value, null);
                }
            }

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
