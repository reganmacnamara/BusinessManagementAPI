using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Products.DeleteProduct;

public class DeleteProductHandler(SQLContext context) : IUseCaseHandler<DeleteProductRequest>
{
    public async Task<IResult> HandleAsync(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        var _Product = await context.GetEntities<Product>()
            .SingleOrDefaultAsync(product => product.ProductID == request.ProductID, cancellationToken);

        if (_Product is null)
            return Results.NotFound("Product not found.");


        var _ProductLines = await context.GetEntities<InvoiceItem>()
                .Where(p => p.ProductID == request.ProductID)
                .ToListAsync(cancellationToken);

        if (_ProductLines.Count != 0)
            return Results.Conflict("Cannot delete a Product that still has history.");

        context.Products.Remove(_Product);

        _ = await context.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }
}
