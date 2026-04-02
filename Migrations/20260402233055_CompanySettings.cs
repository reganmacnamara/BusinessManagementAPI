using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceAutomationAPI.Migrations
{
    /// <inheritdoc />
    public partial class CompanySettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CompanySettingsID",
                table: "Companies",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "CompanySettings",
                columns: table => new
                {
                    CompanySettingsID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoicePrefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NextInvoiceNumber = table.Column<long>(type: "bigint", nullable: false),
                    AutoIncrementInvoice = table.Column<bool>(type: "bit", nullable: false),
                    ReceiptPrefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NextReceiptNumber = table.Column<long>(type: "bigint", nullable: false),
                    AutoIncrementReceipt = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySettings", x => x.CompanySettingsID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanySettingsID",
                table: "Companies",
                column: "CompanySettingsID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_CompanySettings_CompanySettingsID",
                table: "Companies",
                column: "CompanySettingsID",
                principalTable: "CompanySettings",
                principalColumn: "CompanySettingsID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_CompanySettings_CompanySettingsID",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "CompanySettings");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CompanySettingsID",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CompanySettingsID",
                table: "Companies");
        }
    }
}
