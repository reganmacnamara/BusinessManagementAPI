using BusinessManagementAPI.Entities;

namespace BusinessManagementAPI.UseCases.Invoices.GetInvoices;

public class GetInvoicesResponse
{
    public List<Invoice> Invoices { get; set; } = [];
}
