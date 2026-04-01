namespace MacsBusinessManagementAPI.Entities;

public class ReminderLog
{
    public long ReminderLogID { get; set; }

    public long ClientID { get; set; }

    public DateTime SentAt { get; set; }

    public int InvoiceCount { get; set; }

    // Navigation Properties

    public Client Client { get; set; } = default!;
}
