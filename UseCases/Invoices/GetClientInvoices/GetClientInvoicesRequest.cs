using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.GetClientInvoices;

public class GetClientInvoicesRequest : IUseCaseRequest
{
    public long ClientID { get; set; }
}
