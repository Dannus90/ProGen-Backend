using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "refresh_token",
                columns: table => new
                {
                    refresh_token = table.Column<string>(type: "TEXT", nullable: false),
                    id = table.Column<string>(type: "CHAR(36)", nullable: false),
                    user_id = table.Column<string>(type: "Char(36)", nullable: false),
                    token_set_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_token", x => x.refresh_token);
                });

            migrationBuilder.CreateTable(
                name: "user_base",
                columns: table => new
                {
                    id = table.Column<string>(type: "CHAR(36)", nullable: false),
                    email = table.Column<string>(type: "CHAR(128)", nullable: false),
                    password = table.Column<string>(type: "CHAR(500)", nullable: false),
                    last_login = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_base", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_base_email",
                table: "user_base",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "refresh_token");

            migrationBuilder.DropTable(
                name: "user_base");
        }
    }
}
