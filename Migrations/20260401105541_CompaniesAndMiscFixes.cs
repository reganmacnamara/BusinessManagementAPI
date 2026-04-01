using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceAutomationAPI.Migrations
{
    /// <inheritdoc />
    public partial class CompaniesAndMiscFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_Clients_PaymentTermID",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PaymentTermID1",
                table: "Invoices");

            migrationBuilder.AddColumn<long>(
                name: "CompanyID",
                table: "Receipts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ReceiptID1",
                table: "ReceiptItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CompanyID",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyID",
                table: "PaymentTerms",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "PaymentTermID",
                table: "Invoices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CompanyID",
                table: "Invoices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "InvoiceID1",
                table: "InvoiceItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReminderIntervalDays",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PaymentTermID",
                table: "Clients",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CompanyID",
                table: "Clients",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyID",
                table: "Accounts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyABN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_CompanyID",
                table: "Receipts",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptItems_ReceiptID1",
                table: "ReceiptItems",
                column: "ReceiptID1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyID",
                table: "Products",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTerms_CompanyID",
                table: "PaymentTerms",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CompanyID",
                table: "Invoices",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentTermID",
                table: "Invoices",
                column: "PaymentTermID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceID1",
                table: "InvoiceItems",
                column: "InvoiceID1");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CompanyID",
                table: "Clients",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PaymentTermID",
                table: "Clients",
                column: "PaymentTermID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CompanyID",
                table: "Accounts",
                column: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Companies_CompanyID",
                table: "Accounts",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Companies_CompanyID",
                table: "Clients",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_PaymentTerms_PaymentTermID",
                table: "Clients",
                column: "PaymentTermID",
                principalTable: "PaymentTerms",
                principalColumn: "PaymentTermID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_Invoices_InvoiceID1",
                table: "InvoiceItems",
                column: "InvoiceID1",
                principalTable: "Invoices",
                principalColumn: "InvoiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Companies_CompanyID",
                table: "Invoices",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_PaymentTerms_PaymentTermID",
                table: "Invoices",
                column: "PaymentTermID",
                principalTable: "PaymentTerms",
                principalColumn: "PaymentTermID");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTerms_Companies_CompanyID",
                table: "PaymentTerms",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Companies_CompanyID",
                table: "Products",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptItems_Receipts_ReceiptID1",
                table: "ReceiptItems",
                column: "ReceiptID1",
                principalTable: "Receipts",
                principalColumn: "ReceiptID");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Companies_CompanyID",
                table: "Receipts",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "CompanyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Companies_CompanyID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Companies_CompanyID",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_PaymentTerms_PaymentTermID",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_Invoices_InvoiceID1",
                table: "InvoiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Companies_CompanyID",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_PaymentTerms_PaymentTermID",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTerms_Companies_CompanyID",
                table: "PaymentTerms");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Companies_CompanyID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptItems_Receipts_ReceiptID1",
                table: "ReceiptItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Companies_CompanyID",
                table: "Receipts");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_CompanyID",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptItems_ReceiptID1",
                table: "ReceiptItems");

            migrationBuilder.DropIndex(
                name: "IX_Products_CompanyID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTerms_CompanyID",
                table: "PaymentTerms");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CompanyID",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_PaymentTermID",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItems_InvoiceID1",
                table: "InvoiceItems");

            migrationBuilder.DropIndex(
                name: "IX_Clients_CompanyID",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_PaymentTermID",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_CompanyID",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "ReceiptID1",
                table: "ReceiptItems");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "PaymentTerms");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceID1",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "Accounts");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentTermID",
                table: "Invoices",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "PaymentTermID1",
                table: "Invoices",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReminderIntervalDays",
                table: "Clients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PaymentTermID",
                table: "Clients",
                column: "PaymentTermID");

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
    }
}
