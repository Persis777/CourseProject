using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRecommendations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "189dc48b-10d0-4c38-bbc8-ff64918180b1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30200fc7-ec74-4bbf-8091-b9d9cf47c546");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "UserTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "UserRecommendations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecommendationText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    RecommendationType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRecommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRecommendations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1abc68c2-420a-4c8b-89e1-ada5069f3882", null, "Admin", "ADMIN" },
                    { "73a49acc-9544-413c-999f-2001ffb9062f", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRecommendations_UserId",
                table: "UserRecommendations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRecommendations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1abc68c2-420a-4c8b-89e1-ada5069f3882");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73a49acc-9544-413c-999f-2001ffb9062f");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserTasks");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "UserTasks");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "189dc48b-10d0-4c38-bbc8-ff64918180b1", null, "Admin", "ADMIN" },
                    { "30200fc7-ec74-4bbf-8091-b9d9cf47c546", null, "User", "USER" }
                });
        }
    }
}
