using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Project.Data.Migrations
{
    public partial class AddGameOfferModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameOffers",
                columns: table => new
                {
                    GameOfferId = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    GameCategoryId = table.Column<string>(nullable: true),
                    GameTypeId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Wage = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameOffers", x => x.GameOfferId);
                    table.ForeignKey(
                        name: "FK_GameOffers_GameCategories_GameCategoryId",
                        column: x => x.GameCategoryId,
                        principalTable: "GameCategories",
                        principalColumn: "GameCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameOffers_GameTypes_GameTypeId",
                        column: x => x.GameTypeId,
                        principalTable: "GameTypes",
                        principalColumn: "GameTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameOffers_GameCategoryId",
                table: "GameOffers",
                column: "GameCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GameOffers_GameTypeId",
                table: "GameOffers",
                column: "GameTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameOffers");
        }
    }
}
