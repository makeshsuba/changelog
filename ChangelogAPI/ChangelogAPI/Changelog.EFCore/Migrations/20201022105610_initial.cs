using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Changelog.EFCore.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Logs",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    LogTitle = table.Column<string>(nullable: true),
                    LogTypeId = table.Column<int>(nullable: false),
                    LogDescription = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogTypes",
                schema: "dbo",
                columns: table => new
                {
                    LogTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogType = table.Column<string>(nullable: true),
                    IsVisible = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogTypes", x => x.LogTypeId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "LogTypes",
                columns: new[] { "LogTypeId", "IsVisible", "LogType" },
                values: new object[] { 1, true, "Bug" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "LogTypes",
                columns: new[] { "LogTypeId", "IsVisible", "LogType" },
                values: new object[] { 2, true, "Update" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "LogTypes",
                columns: new[] { "LogTypeId", "IsVisible", "LogType" },
                values: new object[] { 3, true, "Feature" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LogTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");
        }
    }
}
