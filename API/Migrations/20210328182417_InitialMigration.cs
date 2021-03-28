using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "user_base",
                table => new
                {
                    id = table.Column<string>("CHAR(36)", nullable: false),
                    email = table.Column<string>("CHAR(128)", nullable: false),
                    password = table.Column<string>("CHAR(500)", nullable: false),
                    last_login = table.Column<DateTime>("timestamp without time zone", nullable: true),
                    created_at = table.Column<DateTime>("timestamp without time zone", nullable: false,
                        defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>("timestamp without time zone", nullable: false,
                        defaultValueSql: "NOW()")
                },
                constraints: table => { table.PrimaryKey("PK_user_base", x => x.id); });

            migrationBuilder.CreateTable(
                "refresh_token",
                table => new
                {
                    id = table.Column<string>("CHAR(36)", nullable: false),
                    user_id = table.Column<string>("Char(36)", nullable: false),
                    refresh_token = table.Column<string>("TEXT", nullable: false),
                    token_set_at = table.Column<DateTime>("timestamp without time zone", nullable: false,
                        defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_token", x => x.id);
                    table.ForeignKey(
                        "FK_refresh_token_user_base_user_id",
                        x => x.user_id,
                        "user_base",
                        "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_refresh_token_refresh_token",
                "refresh_token",
                "refresh_token");

            migrationBuilder.CreateIndex(
                "IX_refresh_token_user_id",
                "refresh_token",
                "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_user_base_email",
                "user_base",
                "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "refresh_token");

            migrationBuilder.DropTable(
                "user_base");
        }
    }
}