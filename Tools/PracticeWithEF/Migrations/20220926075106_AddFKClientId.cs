using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeWithEF.Migrations
{
    public partial class AddFKClientId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accounts_clients_clientDBId",
                table: "accounts");

            migrationBuilder.RenameColumn(
                name: "clientDBId",
                table: "accounts",
                newName: "ClientDBId");

            migrationBuilder.RenameIndex(
                name: "IX_accounts_clientDBId",
                table: "accounts",
                newName: "IX_accounts_ClientDBId");

            migrationBuilder.AddForeignKey(
                name: "FK_accounts_clients_ClientDBId",
                table: "accounts",
                column: "ClientDBId",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accounts_clients_ClientDBId",
                table: "accounts");

            migrationBuilder.RenameColumn(
                name: "ClientDBId",
                table: "accounts",
                newName: "clientDBId");

            migrationBuilder.RenameIndex(
                name: "IX_accounts_ClientDBId",
                table: "accounts",
                newName: "IX_accounts_clientDBId");

            migrationBuilder.AddForeignKey(
                name: "FK_accounts_clients_clientDBId",
                table: "accounts",
                column: "clientDBId",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
