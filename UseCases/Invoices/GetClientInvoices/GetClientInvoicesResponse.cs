using BusinessManagementAPI.Entities;

namespace BusinessManagementAPI.UseCases.Invoices.GetClientInvoices;

public class GetClientInvoicesResponse
{
    public List<Invoice> Invoices { get; set; } = [];
}
