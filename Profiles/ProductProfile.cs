using AutoMapper;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.UseCases.Products.CreateProduct;
using MacsBusinessManagementAPI.UseCases.Products.GetProduct;
using MacsBusinessManagementAPI.UseCases.Products.GetProducts;

namespace MacsBusinessManagementAPI.Profiles;

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
