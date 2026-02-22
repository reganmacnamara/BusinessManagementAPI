using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceAutomationAPI.Migrations
{
    /// <inheritdoc />
    public partial class TransactionSplitAddedInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "TransactionAllocations");

            migrationBuilder.DropTable(
                name: "TransactionItems");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    TransactionID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionRef = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientID = table.Column<long>(type: "bigint", nullable: false),
                    Outstanding = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    GrossValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OffsetValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_Invoices_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    InvoiceItemID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceID = table.Column<long>(type: "bigint", nullable: false),
                    ProductID = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PricePerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Gross = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Net = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.InvoiceItemID);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceID",
                        column: x => x.InvoiceID,
                        principalTable: "Invoices",
                        principalColumn: "TransactionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceID",
                table: "InvoiceItems",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_ProductID",
                table: "InvoiceItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ClientID",
                table: "Invoices",
                column: "ClientID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceGross = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceNet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceID);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<long>(type: "bigint", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GrossValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OffsetValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OffsetingValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Outstanding = table.Column<bool>(type: "bit", nullable: false),
                    OutstandingValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransactionRef = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_Transactions_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionAllocations",
                columns: table => new
                {
                    TransactionAllocationID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AllocatingID = table.Column<long>(type: "bigint", nullable: false),
                    RecievingID = table.Column<long>(type: "bigint", nullable: false),
                    AllocationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AllocationValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionAllocations", x => x.TransactionAllocationID);
                    table.ForeignKey(
                        name: "FK_TransactionAllocations_Transactions_AllocatingID",
                        column: x => x.AllocatingID,
                        principalTable: "Transactions",
                        principalColumn: "TransactionID");
                    table.ForeignKey(
                        name: "FK_TransactionAllocations_Transactions_RecievingID",
                        column: x => x.RecievingID,
                        principalTable: "Transactions",
                        principalColumn: "TransactionID");
                });

            migrationBuilder.CreateTable(
                name: "TransactionItems",
                columns: table => new
                {
                    TransactionItemID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<long>(type: "bigint", nullable: false),
                    TransactionID = table.Column<long>(type: "bigint", nullable: false),
                    LineGross = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LineNet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LineTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PricePerItem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionItems", x => x.TransactionItemID);
                    table.ForeignKey(
                        name: "FK_TransactionItems_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionItems_Transactions_TransactionID",
                        column: x => x.TransactionID,
                        principalTable: "Transactions",
                        principalColumn: "TransactionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionAllocations_AllocatingID",
                table: "TransactionAllocations",
                column: "AllocatingID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionAllocations_RecievingID",
                table: "TransactionAllocations",
                column: "RecievingID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItems_ProductID",
                table: "TransactionItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItems_TransactionID",
                table: "TransactionItems",
                column: "TransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ClientID",
                table: "Transactions",
                column: "ClientID");
        }
    }
}
