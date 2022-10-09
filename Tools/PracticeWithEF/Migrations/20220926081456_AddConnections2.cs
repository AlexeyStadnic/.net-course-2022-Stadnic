using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeWithEF.Migrations
{
    public partial class AddConnections2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_accounts_currency_id",
                table: "accounts",
                column: "currency_id");

            migrationBuilder.AddForeignKey(
                name: "FK_accounts_currencys_currency_id",
                table: "accounts",
                column: "currency_id",
                principalTable: "currencys",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accounts_currencys_currency_id",
                table: "accounts");

            migrationBuilder.DropIndex(
                name: "IX_accounts_currency_id",
                table: "accounts");
        }
    }
}
