using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;

namespace MacsBusinessManagementAPI.UseCases.Products.DeleteProduct;

public class DeleteProductHandler(SQLContext context) : IUseCaseHandler<DeleteProductRequest>
{
    public async Task<IResult> HandleAsync(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        var _Product = context.GetEntities<Product>()
            .SingleOrDefault(product => product.ProductID == request.ProductID);

        if (_Product is null)
            return Results.NotFound("Product not found.");


        var _IsProductUsed = context.GetEntities<InvoiceItem>()
                .Where(p => p.ProductID == request.ProductID)
                .ToList()
                .Count != 0;

        if (_IsProductUsed)
            return Results.Conflict("Cannot delete a Product that still has history.");

        context.Products.Remove(_Product);
        await context.SaveChangesAsync(cancellationToken);
        return Results.NoContent();
    }
}
