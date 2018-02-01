using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Project.Data.Migrations
{
    public partial class jakastam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameOffers_AspNetUsers_AuthorId",
                table: "GameOffers");

            migrationBuilder.DropIndex(
                name: "IX_GameOffers_AuthorId",
                table: "GameOffers");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "GameOffers");

            migrationBuilder.DropColumn(
                name: "LastEdit",
                table: "GameOffers");

            migrationBuilder.DropColumn(
                name: "Submitted",
                table: "GameOffers");

            migrationBuilder.DropColumn(
                name: "Wage",
                table: "GameOffers");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "GameOffers",
                newName: "ApplicationUserId");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "GameOffers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "GameOffers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GameOffers_ApplicationUserId",
                table: "GameOffers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameOffers_AspNetUsers_ApplicationUserId",
                table: "GameOffers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameOffers_AspNetUsers_ApplicationUserId",
                table: "GameOffers");

            migrationBuilder.DropIndex(
                name: "IX_GameOffers_ApplicationUserId",
                table: "GameOffers");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "GameOffers");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "GameOffers",
                newName: "PostalCode");

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "GameOffers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "GameOffers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEdit",
                table: "GameOffers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Submitted",
                table: "GameOffers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Wage",
                table: "GameOffers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_GameOffers_AuthorId",
                table: "GameOffers",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameOffers_AspNetUsers_AuthorId",
                table: "GameOffers",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
