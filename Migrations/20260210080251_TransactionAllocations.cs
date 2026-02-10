using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceAutomationAPI.Migrations
{
    /// <inheritdoc />
    public partial class TransactionAllocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Outstanding",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TransactionAllocations",
                columns: table => new
                {
                    TransactionAllocationID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AllocatingID = table.Column<long>(type: "bigint", nullable: false),
                    RecievingID = table.Column<long>(type: "bigint", nullable: false),
                    AllocationValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AllocationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionAllocationID1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionAllocations", x => x.TransactionAllocationID);
                    table.ForeignKey(
                        name: "FK_TransactionAllocations_TransactionAllocations_TransactionAllocationID1",
                        column: x => x.TransactionAllocationID1,
                        principalTable: "TransactionAllocations",
                        principalColumn: "TransactionAllocationID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionAllocations_TransactionAllocationID1",
                table: "TransactionAllocations",
                column: "TransactionAllocationID1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionAllocations");

            migrationBuilder.DropColumn(
                name: "Outstanding",
                table: "Transactions");
        }
    }
}
