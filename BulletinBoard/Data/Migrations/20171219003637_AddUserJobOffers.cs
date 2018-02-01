using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Project.Data.Migrations
{
    public partial class AddUserGameOffers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "GameOffers",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
