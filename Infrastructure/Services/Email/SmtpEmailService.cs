using MacsBusinessManagementAPI.Entities;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace MacsBusinessManagementAPI.Infrastructure.Services.Email;

public class SmtpEmailService(IOptions<SmtpSettings> smtpSettings) : IEmailService
{
    private readonly SmtpSettings _Settings = smtpSettings.Value;

    public async Task SendOverdueReminderAsync(Client client, Company company, List<Invoice> overdueInvoices, CancellationToken cancellationToken)
    {
        var _Message = new MimeMessage();
        _Message.From.Add(new MailboxAddress(company.CompanyName, string.IsNullOrEmpty(company.Email)
            ? "overduereminder@macsbusinessmanager.com"
            : company.Email));
        _Message.To.Add(new MailboxAddress(client.ClientName, client.ClientEmail));
        _Message.Subject = "Overdue Invoice Reminder";

        var _Body = BuildEmailBody(client, overdueInvoices);
        _Message.Body = new TextPart("html") { Text = _Body };

        using var _Smtp = new SmtpClient();
        await _Smtp.ConnectAsync(_Settings.Host, _Settings.Port, MailKit.Security.SecureSocketOptions.StartTls, cancellationToken);
        await _Smtp.AuthenticateAsync(_Settings.Username, _Settings.Password, cancellationToken);
        await _Smtp.SendAsync(_Message, cancellationToken);
        await _Smtp.DisconnectAsync(true, cancellationToken);
    }

    private static string BuildEmailBody(Client client, List<Invoice> overdueInvoices)
    {
        var rows = string.Join("", overdueInvoices.Select(inv =>
        {
            var daysOverdue = (DateTime.UtcNow.Date - inv.DueDate.Date).Days;
            return $"""
                <tr>
                    <td style="padding: 8px; border: 1px solid #ddd;">{inv.InvoiceRef}</td>
                    <td style="padding: 8px; border: 1px solid #ddd;">{inv.InvoiceDate:dd/MM/yyyy}</td>
                    <td style="padding: 8px; border: 1px solid #ddd;">{inv.DueDate:dd/MM/yyyy}</td>
                    <td style="padding: 8px; border: 1px solid #ddd;">{inv.GrossValue:C}</td>
                    <td style="padding: 8px; border: 1px solid #ddd;">{daysOverdue}</td>
                </tr>
                """;
        }));

        return $"""
            <h2>Overdue Invoice Reminder</h2>
            <p>Dear {client.ClientName},</p>
            <p>The following invoices are currently overdue:</p>
            <table style="border-collapse: collapse; width: 100%;">
                <thead>
                    <tr style="background-color: #f2f2f2;">
                        <th style="padding: 8px; border: 1px solid #ddd;">Reference</th>
                        <th style="padding: 8px; border: 1px solid #ddd;">Invoice Date</th>
                        <th style="padding: 8px; border: 1px solid #ddd;">Due Date</th>
                        <th style="padding: 8px; border: 1px solid #ddd;">Amount</th>
                        <th style="padding: 8px; border: 1px solid #ddd;">Days Overdue</th>
                    </tr>
                </thead>
                <tbody>
                    {rows}
                </tbody>
            </table>
            <p>Please arrange payment at your earliest convenience.</p>
            """;
    }
}
