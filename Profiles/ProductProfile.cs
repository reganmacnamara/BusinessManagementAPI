using AutoMapper;
using InvoiceAutomationAPI.Models;
using InvoiceAutomationAPI.UseCases.Products.CreateProduct;

namespace InvoiceAutomationAPI.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductRequest, Product>();
    }
}
