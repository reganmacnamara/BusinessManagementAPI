using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceAutomationAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedNavigationalConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ServiceName",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ServiceDescription",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "QuantityOnHand",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "ProductDescription",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionAllocations_AllocatingID",
                table: "TransactionAllocations",
                column: "AllocatingID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionAllocations_RecievingID",
                table: "TransactionAllocations",
                column: "RecievingID");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionAllocations_Transactions_AllocatingID",
                table: "TransactionAllocations",
                column: "AllocatingID",
                principalTable: "Transactions",
                principalColumn: "TransactionID");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionAllocations_Transactions_RecievingID",
                table: "TransactionAllocations",
                column: "RecievingID",
                principalTable: "Transactions",
                principalColumn: "TransactionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionAllocations_Transactions_AllocatingID",
                table: "TransactionAllocations");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionAllocations_Transactions_RecievingID",
                table: "TransactionAllocations");

            migrationBuilder.DropIndex(
                name: "IX_TransactionAllocations_AllocatingID",
                table: "TransactionAllocations");

            migrationBuilder.DropIndex(
                name: "IX_TransactionAllocations_RecievingID",
                table: "TransactionAllocations");

            migrationBuilder.AlterColumn<string>(
                name: "ServiceName",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ServiceDescription",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "QuantityOnHand",
                table: "Products",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "ProductDescription",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
