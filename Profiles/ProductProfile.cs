using AutoMapper;
using InvoiceAutomationAPI.Models;
using InvoiceAutomationAPI.UseCases.Products.CreateProduct;
using InvoiceAutomationAPI.UseCases.Products.GetProduct;
using InvoiceAutomationAPI.UseCases.Products.GetProducts;

namespace InvoiceAutomationAPI.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductRequest, Product>();

        CreateMap<List<Product>, GetProductsResponse>()
                .ForMember(response => response.Products, output => output.MapFrom(source => source));

        CreateMap<Product, GetProductResponse>()
            .ForMember(response => response.Product, output => output.MapFrom(source => source));
    }
}
