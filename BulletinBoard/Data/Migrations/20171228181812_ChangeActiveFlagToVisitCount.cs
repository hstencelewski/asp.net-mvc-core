using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Project.Data.Migrations
{
    public partial class ChangeActiveFlagToVisitCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "GameOffers");

            migrationBuilder.AddColumn<int>(
                name: "Visits",
                table: "GameOffers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visits",
                table: "GameOffers");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "GameOffers",
                nullable: false,
                defaultValue: false);
        }
    }
}
