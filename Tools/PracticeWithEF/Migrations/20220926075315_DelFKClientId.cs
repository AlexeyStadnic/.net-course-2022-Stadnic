using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeWithEF.Migrations
{
    public partial class DelFKClientId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accounts_clients_ClientDBId",
                table: "accounts");

            migrationBuilder.DropIndex(
                name: "IX_accounts_ClientDBId",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "ClientDBId",
                table: "accounts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClientDBId",
                table: "accounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_accounts_ClientDBId",
                table: "accounts",
                column: "ClientDBId");

            migrationBuilder.AddForeignKey(
                name: "FK_accounts_clients_ClientDBId",
                table: "accounts",
                column: "ClientDBId",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
