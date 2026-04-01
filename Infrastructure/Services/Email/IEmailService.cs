using MacsBusinessManagementAPI.Entities;

namespace MacsBusinessManagementAPI.Infrastructure.Services.Email;

public interface IEmailService
{
    Task SendOverdueReminderAsync(Client client, Company company, List<Invoice> overdueInvoices, CancellationToken cancellationToken);
}
