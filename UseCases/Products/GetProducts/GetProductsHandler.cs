using AutoMapper;
using InvoiceAutomationAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace InvoiceAutomationAPI.UseCases.Products.GetProducts;

public class GetProductsHandler(IMapper mapper) : BaseHandler(mapper)
{
    public async Task<GetProductsResponse> GetProducts()
        => new() { Products = await m_Context.Products.ToListAsync() };
}
