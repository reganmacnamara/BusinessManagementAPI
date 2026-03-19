using MacsBusinessManagementAPI.Entities;

namespace MacsBusinessManagementAPI.UseCases.Invoices.GetInvoices;

public class GetInvoicesResponse
{
    public List<Invoice> Invoices { get; set; } = [];
}
