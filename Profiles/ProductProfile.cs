using AutoMapper;
using BusinessManagementAPI.Models;
using BusinessManagementAPI.UseCases.Products.CreateProduct;
using BusinessManagementAPI.UseCases.Products.GetProduct;
using BusinessManagementAPI.UseCases.Products.GetProducts;

namespace BusinessManagementAPI.Profiles;

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
