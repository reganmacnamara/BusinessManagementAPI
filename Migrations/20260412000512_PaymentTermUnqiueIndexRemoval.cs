using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceAutomationAPI.Migrations
{
    /// <inheritdoc />
    public partial class PaymentTermUnqiueIndexRemoval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invoices_PaymentTermID",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Clients_PaymentTermID",
                table: "Clients");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentTermID",
                table: "Invoices",
                column: "PaymentTermID");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PaymentTermID",
                table: "Clients",
                column: "PaymentTermID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invoices_PaymentTermID",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Clients_PaymentTermID",
                table: "Clients");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentTermID",
                table: "Invoices",
                column: "PaymentTermID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PaymentTermID",
                table: "Clients",
                column: "PaymentTermID",
                unique: true);
        }
    }
}
