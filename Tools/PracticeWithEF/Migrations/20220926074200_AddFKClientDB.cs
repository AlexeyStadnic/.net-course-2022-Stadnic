using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeWithEF.Migrations
{
    public partial class AddFKClientDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "clientDBId",
                table: "accounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_accounts_clientDBId",
                table: "accounts",
                column: "clientDBId");

            migrationBuilder.AddForeignKey(
                name: "FK_accounts_clients_clientDBId",
                table: "accounts",
                column: "clientDBId",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accounts_clients_clientDBId",
                table: "accounts");

            migrationBuilder.DropIndex(
                name: "IX_accounts_clientDBId",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "clientDBId",
                table: "accounts");
        }
    }
}
