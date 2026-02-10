using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceAutomationAPI.Migrations
{
    /// <inheritdoc />
    public partial class TransactionAllocationsRemoveTransactionsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionAllocations_TransactionAllocations_TransactionAllocationID1",
                table: "TransactionAllocations");

            migrationBuilder.DropIndex(
                name: "IX_TransactionAllocations_TransactionAllocationID1",
                table: "TransactionAllocations");

            migrationBuilder.DropColumn(
                name: "TransactionAllocationID1",
                table: "TransactionAllocations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TransactionAllocationID1",
                table: "TransactionAllocations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionAllocations_TransactionAllocationID1",
                table: "TransactionAllocations",
                column: "TransactionAllocationID1");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionAllocations_TransactionAllocations_TransactionAllocationID1",
                table: "TransactionAllocations",
                column: "TransactionAllocationID1",
                principalTable: "TransactionAllocations",
                principalColumn: "TransactionAllocationID");
        }
    }
}
