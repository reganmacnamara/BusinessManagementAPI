using MacsBusinessManagementAPI.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace MacsBusinessManagementAPI.Services.Pdf;

public class PdfService : IPdfService
{
    private const string PrimaryColor = "#1E3A5F";
    private const string LightGray = "#F5F7FA";
    private const string MidGray = "#E0E0E0";
    private const string TextGray = "#666666";
    private const string AccentBlue = "#2980B9";
    private const string PaidColor = "#27AE60";
    private const string OutstandingColor = "#C0392B";

    public byte[] GenerateInvoicePdf(Invoice invoice, List<InvoiceItem> items)
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.MarginHorizontal(45);
                page.MarginVertical(40);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Arial"));

                page.Content().Column(col =>
                {
                    // ── Header ─────────────────────────────────────────────────
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Column(c =>
                        {
                            c.Item()
                                .DefaultTextStyle(x => x.FontSize(32).Bold().FontColor(PrimaryColor))
                                .Text("INVOICE");
                            c.Item()
                                .DefaultTextStyle(x => x.FontSize(13).FontColor(TextGray))
                                .Text($"# {invoice.InvoiceRef}");
                        });

                        row.ConstantItem(195).Column(c =>
                        {
                            var statusColor = invoice.Outstanding ? OutstandingColor : PaidColor;
                            c.Item()
                                .AlignRight()
                                .DefaultTextStyle(x => x.FontSize(12).Bold().FontColor(statusColor))
                                .Text(invoice.Outstanding ? "OUTSTANDING" : "PAID");
                            c.Item().Height(6);
                            c.Item()
                                .AlignRight()
                                .DefaultTextStyle(x => x.FontColor(TextGray))
                                .Text($"Invoice Date:  {invoice.InvoiceDate:dd MMM yyyy}");
                            c.Item()
                                .AlignRight()
                                .DefaultTextStyle(x => x.FontColor(TextGray))
                                .Text($"Due Date:  {invoice.DueDate:dd MMM yyyy}");
                        });
                    });

                    col.Item().PaddingVertical(14).LineHorizontal(1.5f).LineColor(PrimaryColor);

                    // ── Bill To ─────────────────────────────────────────────────
                    col.Item().PaddingBottom(22).Column(c =>
                    {
                        c.Item()
                            .DefaultTextStyle(x => x.FontSize(8).Bold().FontColor(TextGray))
                            .Text("BILL TO");
                        c.Item().Height(4);
                        c.Item()
                            .DefaultTextStyle(x => x.FontSize(12).Bold())
                            .Text(invoice.Client.ClientName);

                        if (!string.IsNullOrWhiteSpace(invoice.Client.AddressLine1))
                            c.Item().Text(invoice.Client.AddressLine1);
                        if (!string.IsNullOrWhiteSpace(invoice.Client.AddressLine2))
                            c.Item().Text(invoice.Client.AddressLine2);

                        var cityLine = string.Join(", ", new[]
                        {
                            invoice.Client.PostCode,
                            invoice.Client.State,
                            invoice.Client.Country
                        }.Where(s => !string.IsNullOrWhiteSpace(s)));

                        if (!string.IsNullOrWhiteSpace(cityLine))
                            c.Item().Text(cityLine);

                        if (!string.IsNullOrWhiteSpace(invoice.Client.ClientEmail))
                        {
                            c.Item().Height(3);
                            c.Item()
                                .DefaultTextStyle(x => x.FontColor(AccentBlue))
                                .Text(invoice.Client.ClientEmail);
                        }

                        if (!string.IsNullOrWhiteSpace(invoice.Client.ClientPhone))
                            c.Item().Text(invoice.Client.ClientPhone);
                    });

                    // ── Line Items Table ────────────────────────────────────────
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(4.5f);  // Description
                            cols.RelativeColumn(1f);    // Qty
                            cols.RelativeColumn(1.8f);  // Unit Price
                            cols.RelativeColumn(1.5f);  // Tax
                            cols.RelativeColumn(1.8f);  // Net
                        });

                        table.Header(header =>
                        {
                            IContainer HeaderCell(IContainer c) => c
                                .Background(PrimaryColor)
                                .PaddingVertical(8).PaddingHorizontal(6)
                                .DefaultTextStyle(x => x.FontSize(9).Bold().FontColor("#FFFFFF"));

                            header.Cell().Element(HeaderCell).Text("DESCRIPTION");
                            header.Cell().Element(HeaderCell).AlignRight().Text("QTY");
                            header.Cell().Element(HeaderCell).AlignRight().Text("UNIT PRICE");
                            header.Cell().Element(HeaderCell).AlignRight().Text("TAX");
                            header.Cell().Element(HeaderCell).AlignRight().Text("NET");
                        });

                        var isAlternate = false;
                        foreach (var item in items)
                        {
                            isAlternate = !isAlternate;
                            var bg = isAlternate ? LightGray : "#FFFFFF";

                            IContainer BodyCell(IContainer c) => c
                                .Background(bg)
                                .BorderBottom(0.5f).BorderColor(MidGray)
                                .PaddingVertical(7).PaddingHorizontal(6);

                            table.Cell().Element(BodyCell).Column(c =>
                            {
                                c.Item().DefaultTextStyle(x => x.Bold()).Text(item.Description);
                                if (item.Product != null && !string.IsNullOrWhiteSpace(item.Product.ProductCode))
                                    c.Item()
                                        .DefaultTextStyle(x => x.FontSize(8).FontColor(TextGray))
                                        .Text(item.Product.ProductCode);
                            });
                            table.Cell().Element(BodyCell).AlignRight().Text(item.Quantity.ToString("G29"));
                            table.Cell().Element(BodyCell).AlignRight().Text($"${item.PricePerUnit:N2}");
                            table.Cell().Element(BodyCell).AlignRight().Text($"${item.TaxValue:N2}");
                            table.Cell().Element(BodyCell).AlignRight().Text($"${item.NetValue:N2}");
                        }
                    });

                    // ── Totals ──────────────────────────────────────────────────
                    col.Item().PaddingTop(24).AlignRight().Column(c =>
                    {
                        c.Spacing(4);

                        void TotalRow(string label, string value, bool bold = false, string? color = null)
                        {
                            c.Item().MinWidth(250).Row(r =>
                            {
                                r.RelativeItem()
                                    .DefaultTextStyle(x => bold ? x.Bold() : x)
                                    .Text(label);
                                r.ConstantItem(110)
                                    .AlignRight()
                                    .DefaultTextStyle(x => (bold ? x.Bold() : x).FontColor(color ?? "#000000"))
                                    .Text(value);
                            });
                        }

                        TotalRow("Subtotal (Gross):", $"${invoice.GrossValue:N2}");
                        TotalRow("Tax:", $"${invoice.TaxValue:N2}");
                        c.Item().LineHorizontal(0.75f).LineColor(MidGray);
                        TotalRow("Net Total:", $"${invoice.NetValue:N2}", bold: true);
                        TotalRow("Amount Paid:", $"${invoice.OffsetValue:N2}");
                        c.Item().LineHorizontal(1.5f).LineColor(PrimaryColor);

                        var balance = invoice.NetValue - invoice.OffsetValue;
                        TotalRow("Balance Due:", $"${balance:N2}", bold: true,
                            color: balance > 0 ? OutstandingColor : PaidColor);
                    });
                });

                // ── Footer ──────────────────────────────────────────────────────
                page.Footer()
                    .PaddingTop(10)
                    .BorderTop(0.5f).BorderColor(MidGray)
                    .Row(row =>
                    {
                        row.RelativeItem()
                            .DefaultTextStyle(x => x.FontSize(8).FontColor(TextGray))
                            .Text($"Invoice ID: {invoice.InvoiceID}");
                        row.RelativeItem()
                            .AlignRight()
                            .DefaultTextStyle(x => x.FontSize(8).FontColor(TextGray))
                            .Text($"Generated {DateTime.Now:dd MMM yyyy}");
                    });
            });
        }).GeneratePdf();
    }

    public byte[] GenerateReceiptPdf(Receipt receipt, List<ReceiptItem> items)
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.MarginHorizontal(45);
                page.MarginVertical(40);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Arial"));

                page.Content().Column(col =>
                {
                    // ── Header ─────────────────────────────────────────────────
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Column(c =>
                        {
                            c.Item()
                                .DefaultTextStyle(x => x.FontSize(32).Bold().FontColor(PrimaryColor))
                                .Text("RECEIPT");
                            c.Item()
                                .DefaultTextStyle(x => x.FontSize(13).FontColor(TextGray))
                                .Text($"# {receipt.ReceiptRef}");
                        });

                        row.ConstantItem(195).Column(c =>
                        {
                            var statusColor = receipt.Outstanding ? OutstandingColor : PaidColor;
                            c.Item()
                                .AlignRight()
                                .DefaultTextStyle(x => x.FontSize(12).Bold().FontColor(statusColor))
                                .Text(receipt.Outstanding ? "OUTSTANDING" : "SETTLED");
                            c.Item().Height(6);
                            c.Item()
                                .AlignRight()
                                .DefaultTextStyle(x => x.FontColor(TextGray))
                                .Text($"Receipt Date:  {receipt.ReceiptDate:dd MMM yyyy}");
                        });
                    });

                    col.Item().PaddingVertical(14).LineHorizontal(1.5f).LineColor(PrimaryColor);

                    // ── Received From ───────────────────────────────────────────
                    col.Item().PaddingBottom(22).Column(c =>
                    {
                        c.Item()
                            .DefaultTextStyle(x => x.FontSize(8).Bold().FontColor(TextGray))
                            .Text("RECEIVED FROM");
                        c.Item().Height(4);
                        c.Item()
                            .DefaultTextStyle(x => x.FontSize(12).Bold())
                            .Text(receipt.Client.ClientName);

                        if (!string.IsNullOrWhiteSpace(receipt.Client.AddressLine1))
                            c.Item().Text(receipt.Client.AddressLine1);
                        if (!string.IsNullOrWhiteSpace(receipt.Client.AddressLine2))
                            c.Item().Text(receipt.Client.AddressLine2);

                        var cityLine = string.Join(", ", new[]
                        {
                            receipt.Client.PostCode,
                            receipt.Client.State,
                            receipt.Client.Country
                        }.Where(s => !string.IsNullOrWhiteSpace(s)));

                        if (!string.IsNullOrWhiteSpace(cityLine))
                            c.Item().Text(cityLine);

                        if (!string.IsNullOrWhiteSpace(receipt.Client.ClientEmail))
                        {
                            c.Item().Height(3);
                            c.Item()
                                .DefaultTextStyle(x => x.FontColor(AccentBlue))
                                .Text(receipt.Client.ClientEmail);
                        }

                        if (!string.IsNullOrWhiteSpace(receipt.Client.ClientPhone))
                            c.Item().Text(receipt.Client.ClientPhone);
                    });

                    // ── Allocations Table ───────────────────────────────────────
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(4f);   // Invoice Ref
                            cols.RelativeColumn(2f);   // Gross
                            cols.RelativeColumn(2f);   // Tax
                            cols.RelativeColumn(2f);   // Net
                        });

                        table.Header(header =>
                        {
                            IContainer HeaderCell(IContainer c) => c
                                .Background(PrimaryColor)
                                .PaddingVertical(8).PaddingHorizontal(6)
                                .DefaultTextStyle(x => x.FontSize(9).Bold().FontColor("#FFFFFF"));

                            header.Cell().Element(HeaderCell).Text("APPLIED TO INVOICE");
                            header.Cell().Element(HeaderCell).AlignRight().Text("GROSS");
                            header.Cell().Element(HeaderCell).AlignRight().Text("TAX");
                            header.Cell().Element(HeaderCell).AlignRight().Text("NET");
                        });

                        var isAlternate = false;
                        foreach (var item in items)
                        {
                            isAlternate = !isAlternate;
                            var bg = isAlternate ? LightGray : "#FFFFFF";

                            IContainer BodyCell(IContainer c) => c
                                .Background(bg)
                                .BorderBottom(0.5f).BorderColor(MidGray)
                                .PaddingVertical(7).PaddingHorizontal(6);

                            var invoiceRef = item.Invoice != null
                                ? $"Invoice # {item.Invoice.InvoiceRef}"
                                : $"Invoice ID {item.InvoiceID}";

                            table.Cell().Element(BodyCell).Text(invoiceRef);
                            table.Cell().Element(BodyCell).AlignRight().Text($"${item.GrossValue:N2}");
                            table.Cell().Element(BodyCell).AlignRight().Text($"${item.TaxValue:N2}");
                            table.Cell().Element(BodyCell).AlignRight().Text($"${item.NetValue:N2}");
                        }
                    });

                    // ── Totals ──────────────────────────────────────────────────
                    col.Item().PaddingTop(24).AlignRight().Column(c =>
                    {
                        c.Spacing(4);

                        void TotalRow(string label, string value, bool bold = false, string? color = null)
                        {
                            c.Item().MinWidth(250).Row(r =>
                            {
                                r.RelativeItem()
                                    .DefaultTextStyle(x => bold ? x.Bold() : x)
                                    .Text(label);
                                r.ConstantItem(110)
                                    .AlignRight()
                                    .DefaultTextStyle(x => (bold ? x.Bold() : x).FontColor(color ?? "#000000"))
                                    .Text(value);
                            });
                        }

                        TotalRow("Gross:", $"${receipt.GrossValue:N2}");
                        TotalRow("Tax:", $"${receipt.TaxValue:N2}");
                        c.Item().LineHorizontal(0.75f).LineColor(MidGray);
                        TotalRow("Net Total:", $"${receipt.NetValue:N2}", bold: true);
                        TotalRow("Amount Allocated:", $"${receipt.OffsetValue:N2}");

                        var unallocated = receipt.NetValue - receipt.OffsetValue;
                        if (unallocated > 0)
                        {
                            c.Item().LineHorizontal(1.5f).LineColor(PrimaryColor);
                            TotalRow("Unallocated:", $"${unallocated:N2}", bold: true, color: OutstandingColor);
                        }
                    });
                });

                // ── Footer ──────────────────────────────────────────────────────
                page.Footer()
                    .PaddingTop(10)
                    .BorderTop(0.5f).BorderColor(MidGray)
                    .Row(row =>
                    {
                        row.RelativeItem()
                            .DefaultTextStyle(x => x.FontSize(8).FontColor(TextGray))
                            .Text($"Receipt ID: {receipt.ReceiptID}");
                        row.RelativeItem()
                            .AlignRight()
                            .DefaultTextStyle(x => x.FontSize(8).FontColor(TextGray))
                            .Text($"Generated {DateTime.Now:dd MMM yyyy}");
                    });
            });
        }).GeneratePdf();
    }
}
