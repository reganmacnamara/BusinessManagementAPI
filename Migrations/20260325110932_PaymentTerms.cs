using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceAutomationAPI.Migrations
{
    /// <inheritdoc />
    public partial class PaymentTerms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PaymentTermID",
                table: "Clients",
                type: "bigint",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "PaymentTerms",
                columns: table => new
                {
                    PaymentTermID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentTermName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    IsEndOf = table.Column<bool>(type: "bit", nullable: false),
                    IsStartingNext = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTerms", x => x.PaymentTermID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PaymentTermID",
                table: "Clients",
                column: "PaymentTermID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_PaymentTerms_PaymentTermID",
                table: "Clients",
                column: "PaymentTermID",
                principalTable: "PaymentTerms",
                principalColumn: "PaymentTermID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_PaymentTerms_PaymentTermID",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "PaymentTerms");

            migrationBuilder.DropIndex(
                name: "IX_Clients_PaymentTermID",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PaymentTermID",
                table: "Clients");
        }
    }
}
