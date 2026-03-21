using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Base;

namespace MacsBusinessManagementAPI.UseCases.Products.DeleteProduct;

public class DeleteProductHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<DeleteProductRequest>
{
    public async Task<IResult> HandleAsync(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        var _Product = m_Context.GetEntities<Product>()
            .SingleOrDefault(product => product.ProductID == request.ProductID);

        if (_Product is null)
            return Results.NotFound("Product not found.");


        var _IsProductUsed = m_Context.GetEntities<InvoiceItem>()
                .Where(p => p.ProductID == request.ProductID)
                .ToList()
                .Count != 0;

        if (_IsProductUsed)
            return Results.Conflict("Cannot delete a Product that still has history.");

        m_Context.Products.Remove(_Product);
        await m_Context.SaveChangesAsync(cancellationToken);
        return Results.NoContent();
    }
}
