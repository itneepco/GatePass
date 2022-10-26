using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GatePass.Infrastructure.Data.Migrations
{
    public partial class LocationAddedToPass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "SinglePasses",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "MultiplePasses",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SinglePasses_LocationId",
                table: "SinglePasses",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiplePasses_LocationId",
                table: "MultiplePasses",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MultiplePasses_Locations_LocationId",
                table: "MultiplePasses",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SinglePasses_Locations_LocationId",
                table: "SinglePasses",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultiplePasses_Locations_LocationId",
                table: "MultiplePasses");

            migrationBuilder.DropForeignKey(
                name: "FK_SinglePasses_Locations_LocationId",
                table: "SinglePasses");

            migrationBuilder.DropIndex(
                name: "IX_SinglePasses_LocationId",
                table: "SinglePasses");

            migrationBuilder.DropIndex(
                name: "IX_MultiplePasses_LocationId",
                table: "MultiplePasses");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "SinglePasses");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "MultiplePasses");
        }
    }
}
