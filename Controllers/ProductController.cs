using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using MacsBusinessManagementAPI.UseCases.Products.CreateProduct;
using MacsBusinessManagementAPI.UseCases.Products.DeleteProduct;
using MacsBusinessManagementAPI.UseCases.Products.GetProduct;
using MacsBusinessManagementAPI.UseCases.Products.GetProducts;
using MacsBusinessManagementAPI.UseCases.Products.UpdateProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace MacsBusinessManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
[EnableRateLimiting("Authenticated")]
public class ProductController : ControllerBase
{
    [HttpPost]
    public async Task<IResult> CreateProduct([FromBody] CreateProductRequest request,
        [FromServices] UseCaseMediator<CreateProductRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(request, cancellationToken);

    [HttpDelete("{productID}")]
    public async Task<IResult> DeleteProduct([FromRoute] long productID,
        [FromServices] UseCaseMediator<DeleteProductRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(new() { ProductID = productID }, cancellationToken);

    [HttpGet("{productID}")]
    public async Task<IResult> GetProduct([FromRoute] long productID,
        [FromServices] UseCaseMediator<GetProductRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(new() { ProductID = productID }, cancellationToken);

    [HttpGet]
    public async Task<IResult> GetProducts(
        [FromServices] UseCaseMediator<GetProductsRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(new(), cancellationToken);

    [HttpPatch]
    public async Task<IResult> UpdateProduct([FromBody] UpdateProductRequest request,
        [FromServices] UseCaseMediator<UpdateProductRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(request, cancellationToken);
}
