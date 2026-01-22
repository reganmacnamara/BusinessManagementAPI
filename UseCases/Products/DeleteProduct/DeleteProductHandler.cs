using AutoMapper;
using InvoiceAutomationAPI.UseCases.Base;

namespace InvoiceAutomationAPI.UseCases.Products.DeleteProduct;

public class DeleteProductHandler(IMapper mapper) : BaseHandler(mapper)
{
    public async Task DeleteProduct(DeleteProductRequest request)
    {
        var _Product = m_Context.Products.FirstOrDefault(product => product.ProductID == request.ProductID);

        if (_Product is not null)
        {
            m_Context.Products.Remove(_Product);
            await m_Context.SaveChangesAsync();
        }
    }
}
