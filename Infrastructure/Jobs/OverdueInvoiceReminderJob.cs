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

        var _Companies = await context.GetEntities<Company>()
            .AsNoTracking()
            .IgnoreQueryFilters()
            .Include(c => c.Clients)
            .ToListAsync(cancellationToken);

        foreach (var _Company in _Companies)
            foreach (var _Client in _Company.Clients.Where(c => c.ReminderIntervalDays != 0 && c.ClientEmail != ""))
            {
                var _LastReminder = await context.GetEntities<ReminderLog>()
                    .AsNoTracking()
                    .IgnoreQueryFilters()
                    .Where(r => r.ClientID == _Client.ClientID)
                    .OrderByDescending(r => r.SentAt)
                    .FirstOrDefaultAsync(cancellationToken);

                if (_LastReminder is not null &&
                    (_Today - _LastReminder.SentAt.Date).Days < _Client.ReminderIntervalDays)
                    continue;

                var _OverdueInvoices = await context.GetEntities<Invoice>()
                    .AsNoTracking()
                    .IgnoreQueryFilters()
                    .Where(i => i.ClientID == _Client.ClientID
                             && i.Outstanding
                             && i.DueDate < _Today)
                    .ToListAsync(cancellationToken);

                if (_OverdueInvoices.Count == 0)
                    continue;

                await emailService.SendOverdueReminderAsync(_Client, _Company, _OverdueInvoices, cancellationToken);

                context.ReminderLogs.Add(new ReminderLog
                {
                    ClientID = _Client.ClientID,
                    SentAt = DateTime.UtcNow,
                    InvoiceCount = _OverdueInvoices.Count
                });

            }

        _ = await context.SaveChangesAsync(cancellationToken);
    }
}
