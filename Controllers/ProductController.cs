using AutoMapper;
using BusinessManagementAPI.UseCases.Products.CreateProduct;
using BusinessManagementAPI.UseCases.Products.DeleteProduct;
using BusinessManagementAPI.UseCases.Products.UpdateProduct;
using InvoiceAutomationAPI.UseCases.Products.DeleteProduct;
using InvoiceAutomationAPI.UseCases.Products.GetProduct;
using InvoiceAutomationAPI.UseCases.Products.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IMapper mapper) : ControllerBase
{
    [HttpPost("Create")]
    public async Task<IResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        var handler = new CreateProductHandler(mapper);

        var _Response = await handler.CreateProduct(request);

        return _Response is CreateProductResponse
            ? Results.Ok(_Response)
            : Results.BadRequest();
    }

    [HttpPost("Delete")]
    public async Task<IResult> DeleteProduct([FromBody] DeleteProductRequest request)
    {
        var handler = new DeleteProductHandler(mapper);

        await handler.DeleteProduct(request);

        return Results.NoContent();
    }

    [HttpGet("{productID}")]
    public async Task<IResult> GetProduct([FromRoute] long productID)
    {
        var _Request = new GetProductRequest()
        {
            ProductID = productID
        };

        var handler = new GetProductHandler(mapper);

        var _Result = await handler.GetProduct(_Request);

        return _Result is not null
            ? Results.Ok(_Result)
            : Results.NotFound();
    }

    [HttpGet]
    public async Task<IResult> GetProducts()
    {
        var handler = new GetProductsHandler(mapper);

        var _Result = await handler.GetProducts();

        return Results.Ok(_Result);
    }

    [HttpPost("Update")]
    public async Task<IResult> UpdateProduct([FromBody] UpdateProductRequest request)
    {
        var handler = new UpdateProductHandler(mapper);

        var _Result = await handler.UpdateProduct(request);

        return _Result.ProductId != 0
            ? Results.Ok(_Result)
            : Results.NotFound();
    }
}
