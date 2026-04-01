using MacsBusinessManagementAPI.Entities;

namespace MacsBusinessManagementAPI.Infrastructure.Services.DueDate;

public static class DueDateCalculator
{
    public static DateTime CalculateDueDate(DateTime invoiceDate, PaymentTerm paymentTerm)
    {
        var date = invoiceDate;

        if (paymentTerm.OffsetFirst)
        {
            date = ApplyOffset(date, paymentTerm.Days, paymentTerm.Months);
            date = ApplySnap(date, paymentTerm.EndOfMonth, paymentTerm.DayOfMonth);
        }
        else
        {
            date = ApplySnap(date, paymentTerm.EndOfMonth, paymentTerm.DayOfMonth);
            date = ApplyOffset(date, paymentTerm.Days, paymentTerm.Months);
        }

        return date;
    }

    private static DateTime ApplyOffset(DateTime date, int days, int months)
        => date.AddMonths(months).AddDays(days);

    private static DateTime ApplySnap(DateTime date, bool endOfMonth, int? dayOfMonth)
    {
        if (endOfMonth)
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

        if (dayOfMonth.HasValue)
        {
            var day = Math.Min(dayOfMonth.Value, DateTime.DaysInMonth(date.Year, date.Month));
            return new DateTime(date.Year, date.Month, day);
        }

        return date;
    }
}
