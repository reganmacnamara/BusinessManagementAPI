using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Services.Email;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.Infrastructure.Jobs;

public class OverdueInvoiceReminderJob(SQLContext context, IEmailService emailService)
{
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var _Today = DateTime.UtcNow.Date;

        var _Clients = await context.GetEntities<Client>()
            .Where(c => c.ReminderIntervalDays != null && c.ClientEmail != "")
            .ToListAsync(cancellationToken);

        foreach (var _Client in _Clients)
        {
            var _LastReminder = await context.GetEntities<ReminderLog>()
                .Where(r => r.ClientID == _Client.ClientID)
                .OrderByDescending(r => r.SentAt)
                .FirstOrDefaultAsync(cancellationToken);

            if (_LastReminder is not null &&
                (_Today - _LastReminder.SentAt.Date).Days < _Client.ReminderIntervalDays)
                continue;

            var overdueInvoices = await context.GetEntities<Invoice>()
                .Where(i => i.ClientID == _Client.ClientID
                         && i.Outstanding
                         && i.DueDate < _Today)
                .ToListAsync(cancellationToken);

            if (overdueInvoices.Count == 0)
                continue;

            await emailService.SendOverdueReminderAsync(_Client, overdueInvoices, cancellationToken);

            context.ReminderLogs.Add(new ReminderLog
            {
                ClientID = _Client.ClientID,
                SentAt = DateTime.UtcNow,
                InvoiceCount = overdueInvoices.Count
            });

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
