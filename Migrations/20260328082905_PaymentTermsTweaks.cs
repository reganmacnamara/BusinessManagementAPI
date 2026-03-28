using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceAutomationAPI.Migrations
{
    /// <inheritdoc />
    public partial class PaymentTermsTweaks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_PaymentTerms_PaymentTermID",
                table: "Clients");

            migrationBuilder.AddColumn<int>(
                name: "PaymentTermID",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PaymentTermID1",
                table: "Invoices",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PaymentTermID",
                table: "Clients",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentTermID1",
                table: "Invoices",
                column: "PaymentTermID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_PaymentTerms_PaymentTermID",
                table: "Clients",
                column: "PaymentTermID",
                principalTable: "PaymentTerms",
                principalColumn: "PaymentTermID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_PaymentTerms_PaymentTermID1",
                table: "Invoices",
                column: "PaymentTermID1",
                principalTable: "PaymentTerms",
                principalColumn: "PaymentTermID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_PaymentTerms_PaymentTermID",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_PaymentTerms_PaymentTermID1",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_PaymentTermID1",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PaymentTermID",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PaymentTermID1",
                table: "Invoices");

            migrationBuilder.AlterColumn<long>(
                name: "PaymentTermID",
                table: "Clients",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_PaymentTerms_PaymentTermID",
                table: "Clients",
                column: "PaymentTermID",
                principalTable: "PaymentTerms",
                principalColumn: "PaymentTermID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
