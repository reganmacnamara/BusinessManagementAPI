using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceAutomationAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedInvoiceProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionRef",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "TransactionType",
                table: "Invoices",
                newName: "InvoiceRef");

            migrationBuilder.RenameColumn(
                name: "TransactionDate",
                table: "Invoices",
                newName: "InvoiceDate");

            migrationBuilder.RenameColumn(
                name: "TransactionID",
                table: "Invoices",
                newName: "InvoiceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvoiceRef",
                table: "Invoices",
                newName: "TransactionType");

            migrationBuilder.RenameColumn(
                name: "InvoiceDate",
                table: "Invoices",
                newName: "TransactionDate");

            migrationBuilder.RenameColumn(
                name: "InvoiceID",
                table: "Invoices",
                newName: "TransactionID");

            migrationBuilder.AddColumn<string>(
                name: "TransactionRef",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
