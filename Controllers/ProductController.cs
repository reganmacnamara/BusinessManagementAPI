using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Products.CreateProduct;
using MacsBusinessManagementAPI.UseCases.Products.DeleteProduct;
using MacsBusinessManagementAPI.UseCases.Products.GetProduct;
using MacsBusinessManagementAPI.UseCases.Products.GetProducts;
using MacsBusinessManagementAPI.UseCases.Products.UpdateProduct;
using Microsoft.AspNetCore.Mvc;

namespace MacsBusinessManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    [HttpPost]
    public async Task<IResult> CreateProduct([FromBody] CreateProductRequest request,
        [FromServices] IUseCaseHandler<CreateProductRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Response = await handler.HandleAsync(request, cancellationToken);

        return _Response;
    }

    [HttpDelete("{productID}")]
    public async Task<IResult> DeleteProduct([FromRoute] long productID,
        [FromServices] IUseCaseHandler<DeleteProductRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Response = await handler.HandleAsync(new() { ProductID = productID }, cancellationToken);

        return _Response;
    }

    [HttpGet("{productID}")]
    public async Task<IResult> GetProduct([FromRoute] long productID,
        [FromServices] IUseCaseHandler<GetProductRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Result = await handler.HandleAsync(new() { ProductID = productID }, cancellationToken);

        return _Result;
    }

    [HttpGet]
    public async Task<IResult> GetProducts([FromServices] IUseCaseHandler<GetProductsRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Result = await handler.HandleAsync(new(), cancellationToken);

        return _Result;
    }

    [HttpPatch]
    public async Task<IResult> UpdateProduct([FromBody] UpdateProductRequest request,
        [FromServices] IUseCaseHandler<UpdateProductRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Result = await handler.HandleAsync(request, cancellationToken);

        return _Result;
    }
}
