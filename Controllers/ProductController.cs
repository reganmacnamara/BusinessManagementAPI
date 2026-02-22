using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Products.CreateProduct;
using BusinessManagementAPI.UseCases.Products.DeleteProduct;
using BusinessManagementAPI.UseCases.Products.GetProduct;
using BusinessManagementAPI.UseCases.Products.GetProducts;
using BusinessManagementAPI.UseCases.Products.UpdateProduct;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IMapper mapper, SQLContext context) : ControllerBase
{
    [HttpPost]
    public async Task<IResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        var handler = new CreateProductHandler(mapper, context);

        var _Response = await handler.CreateProduct(request);

        return _Response;
    }

    [HttpDelete]
    public async Task<IResult> DeleteProduct([FromBody] DeleteProductRequest request)
    {
        var handler = new DeleteProductHandler(mapper, context);

        var _Response = await handler.DeleteProduct(request);

        return _Response;
    }

    [HttpGet("{productID}")]
    public async Task<IResult> GetProduct([FromRoute] long productID)
    {
        var _Request = new GetProductRequest()
        {
            ProductID = productID
        };

        var handler = new GetProductHandler(mapper, context);

        var _Result = await handler.GetProduct(_Request);

        return _Result;
    }

    [HttpGet]
    public async Task<IResult> GetProducts()
    {
        var handler = new GetProductsHandler(mapper, context);

        var _Result = await handler.GetProducts();

        return _Result;
    }

    [HttpPatch]
    public async Task<IResult> UpdateProduct([FromBody] UpdateProductRequest request)
    {
        var handler = new UpdateProductHandler(mapper, context);

        var _Result = await handler.UpdateProduct(request);

        return _Result;
    }
}
