using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlyIBooking.Migrations
{
    public partial class AddNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Accounts_AccountId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Planes_PlaneId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_AccountId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_PlaneId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_Id",
                table: "Accounts");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Tickets",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Id",
                table: "Tickets",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tickets_Id",
                table: "Tickets");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Tickets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AccountId",
                table: "Tickets",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PlaneId",
                table: "Tickets",
                column: "PlaneId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Id",
                table: "Accounts",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Accounts_AccountId",
                table: "Tickets",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Planes_PlaneId",
                table: "Tickets",
                column: "PlaneId",
                principalTable: "Planes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
