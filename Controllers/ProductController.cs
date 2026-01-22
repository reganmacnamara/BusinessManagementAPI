using AutoMapper;
using InvoiceAutomationAPI.UseCases.Products.CreateProduct;
using InvoiceAutomationAPI.UseCases.Products.DeleteProduct;
using InvoiceAutomationAPI.UseCases.Products.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceAutomationAPI.Controllers;

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

    [HttpGet]
    public async Task<IResult> GetProducts()
    {
        var handler = new GetProductsHandler(mapper);

        var _Result = await handler.GetProducts();

        return Results.Ok(_Result);
    }
}
