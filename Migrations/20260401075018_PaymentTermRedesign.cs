using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceAutomationAPI.Migrations
{
    /// <inheritdoc />
    public partial class PaymentTermRedesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "PaymentTerms",
                newName: "Months");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "PaymentTerms",
                newName: "Days");

            migrationBuilder.RenameColumn(
                name: "IsStartingNext",
                table: "PaymentTerms",
                newName: "OffsetFirst");

            migrationBuilder.RenameColumn(
                name: "IsEndOf",
                table: "PaymentTerms",
                newName: "EndOfMonth");

            migrationBuilder.AddColumn<int>(
                name: "DayOfMonth",
                table: "PaymentTerms",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfMonth",
                table: "PaymentTerms");

            migrationBuilder.RenameColumn(
                name: "OffsetFirst",
                table: "PaymentTerms",
                newName: "IsStartingNext");

            migrationBuilder.RenameColumn(
                name: "Months",
                table: "PaymentTerms",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "EndOfMonth",
                table: "PaymentTerms",
                newName: "IsEndOf");

            migrationBuilder.RenameColumn(
                name: "Days",
                table: "PaymentTerms",
                newName: "Unit");
        }
    }
}
