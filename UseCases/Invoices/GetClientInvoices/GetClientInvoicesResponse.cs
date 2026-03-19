using MacsBusinessManagementAPI.Entities;

namespace MacsBusinessManagementAPI.UseCases.Invoices.GetClientInvoices;

public class GetClientInvoicesResponse
{
    public List<Invoice> Invoices { get; set; } = [];
}
