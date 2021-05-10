﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_base",
                columns: table => new
                {
                    id = table.Column<string>(type: "CHAR(36)", nullable: false),
                    email = table.Column<string>(type: "CHAR(128)", nullable: false),
                    password = table.Column<string>(type: "CHAR(500)", nullable: false),
                    first_name = table.Column<string>(type: "CHAR(128)", nullable: false),
                    last_name = table.Column<string>(type: "CHAR(128)", nullable: false),
                    last_login = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_base", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "education",
                columns: table => new
                {
                    id = table.Column<string>(type: "CHAR(36)", nullable: false),
                    user_id = table.Column<string>(type: "Char(36)", nullable: false),
                    education_name_sv = table.Column<string>(type: "Char(108)", nullable: false),
                    education_name_en = table.Column<string>(type: "Char(108)", nullable: false),
                    exam_name_sv = table.Column<string>(type: "Char(108)", nullable: false),
                    exam_name_en = table.Column<string>(type: "Char(108)", nullable: false),
                    subject_area_sv = table.Column<string>(type: "Char(108)", nullable: false),
                    subject_area_en = table.Column<string>(type: "Char(108)", nullable: false),
                    description_sv = table.Column<string>(type: "TEXT", nullable: false),
                    description_en = table.Column<string>(type: "TEXT", nullable: false),
                    grade = table.Column<string>(type: "Char(72)", nullable: false),
                    city_sv = table.Column<string>(type: "Char(72)", nullable: false),
                    city_en = table.Column<string>(type: "Char(72)", nullable: false),
                    country_sv = table.Column<string>(type: "Char(72)", nullable: false),
                    country_en = table.Column<string>(type: "Char(72)", nullable: false),
                    date_started = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    date_ended = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_education", x => x.id);
                    table.ForeignKey(
                        name: "FK_education_user_base_user_id",
                        column: x => x.user_id,
                        principalTable: "user_base",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "language",
                columns: table => new
                {
                    id = table.Column<string>(type: "CHAR(36)", nullable: false),
                    user_id = table.Column<string>(type: "Char(36)", nullable: false),
                    language_sv = table.Column<string>(type: "Char(128)", nullable: false),
                    language_en = table.Column<string>(type: "Char(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_language", x => x.id);
                    table.ForeignKey(
                        name: "FK_language_user_base_user_id",
                        column: x => x.user_id,
                        principalTable: "user_base",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "other_information",
                columns: table => new
                {
                    id = table.Column<string>(type: "CHAR(36)", nullable: false),
                    user_id = table.Column<string>(type: "Char(36)", nullable: false),
                    driving_license_sv = table.Column<string>(type: "TEXT", nullable: false),
                    driving_license_en = table.Column<string>(type: "TEXT", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_other_information", x => x.id);
                    table.ForeignKey(
                        name: "FK_other_information_user_base_user_id",
                        column: x => x.user_id,
                        principalTable: "user_base",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refresh_token",
                columns: table => new
                {
                    id = table.Column<string>(type: "CHAR(36)", nullable: false),
                    user_id = table.Column<string>(type: "Char(36)", nullable: false),
                    refresh_token = table.Column<string>(type: "TEXT", nullable: false),
                    token_set_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_token", x => x.id);
                    table.ForeignKey(
                        name: "FK_refresh_token_user_base_user_id",
                        column: x => x.user_id,
                        principalTable: "user_base",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_data",
                columns: table => new
                {
                    id = table.Column<string>(type: "CHAR(36)", nullable: false),
                    user_id = table.Column<string>(type: "Char(36)", nullable: false),
                    phone_number = table.Column<string>(type: "CHAR(64)", nullable: true),
                    email_cv = table.Column<string>(type: "CHAR(128)", nullable: true),
                    city_sv = table.Column<string>(type: "CHAR(128)", nullable: true),
                    city_en = table.Column<string>(type: "CHAR(128)", nullable: true),
                    country_sv = table.Column<string>(type: "CHAR(128)", nullable: true),
                    country_en = table.Column<string>(type: "CHAR(128)", nullable: true),
                    profile_image = table.Column<string>(type: "CHAR(256)", nullable: true),
                    profile_image_public_id = table.Column<string>(type: "CHAR(36)", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_data", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_data_user_base_user_id",
                        column: x => x.user_id,
                        principalTable: "user_base",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_presentation",
                columns: table => new
                {
                    id = table.Column<string>(type: "CHAR(36)", nullable: false),
                    user_id = table.Column<string>(type: "Char(36)", nullable: false),
                    presentation_sv = table.Column<string>(type: "TEXT", nullable: false),
                    presentation_en = table.Column<string>(type: "TEXT", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_presentation", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_presentation_user_base_user_id",
                        column: x => x.user_id,
                        principalTable: "user_base",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "work_experience",
                columns: table => new
                {
                    id = table.Column<string>(type: "CHAR(36)", nullable: false),
                    user_id = table.Column<string>(type: "Char(36)", nullable: false),
                    employment_rate = table.Column<string>(type: "Char(54)", nullable: false),
                    company_name = table.Column<string>(type: "Char(54)", nullable: false),
                    role_sv = table.Column<string>(type: "Char(108)", nullable: false),
                    role_en = table.Column<string>(type: "Char(108)", nullable: false),
                    description_sv = table.Column<string>(type: "TEXT", nullable: false),
                    description_en = table.Column<string>(type: "TEXT", nullable: false),
                    city_sv = table.Column<string>(type: "Char(72)", nullable: false),
                    city_en = table.Column<string>(type: "Char(72)", nullable: false),
                    country_sv = table.Column<string>(type: "Char(72)", nullable: false),
                    country_en = table.Column<string>(type: "Char(72)", nullable: false),
                    date_started = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    date_ended = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_experience", x => x.id);
                    table.ForeignKey(
                        name: "FK_work_experience_user_base_user_id",
                        column: x => x.user_id,
                        principalTable: "user_base",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "user_base",
                columns: new[] { "id", "email", "first_name", "last_login", "last_name", "password" },
                values: new object[] { "2ce44222-b26b-4b2b-90c5-58a899e191c4", "testuser@gmail.com", "John", null, "Doe", "$2a$10$lmiYrmWUDf7klCsGo0VP.uI9DcK.5fUy2Ld34ahg8lQnIanlzThcy" });

            migrationBuilder.InsertData(
                table: "user_data",
                columns: new[] { "id", "city_en", "city_sv", "country_en", "country_sv", "email_cv", "phone_number", "profile_image", "profile_image_public_id", "user_id" },
                values: new object[] { "1f5c890d-650a-493f-b68e-fc246065ae6a", "Gothenburg", "Göteborg", "Sweden", "Sverige", "persson.daniel.1990@gmail.com", "073-3249826", "", null, "2ce44222-b26b-4b2b-90c5-58a899e191c4" });

            migrationBuilder.InsertData(
                table: "user_presentation",
                columns: new[] { "id", "presentation_en", "presentation_sv", "user_id" },
                values: new object[] { "98882bc6-7dd2-416c-8b6e-373f3f18b4e0", "PresentationText En", "PresentationText Sv", "2ce44222-b26b-4b2b-90c5-58a899e191c4" });

            migrationBuilder.CreateIndex(
                name: "IX_education_user_id",
                table: "education",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_language_user_id",
                table: "language",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_other_information_user_id",
                table: "other_information",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_refresh_token_refresh_token",
                table: "refresh_token",
                column: "refresh_token");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_token_user_id",
                table: "refresh_token",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_base_email",
                table: "user_base",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_data_user_id",
                table: "user_data",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_presentation_user_id",
                table: "user_presentation",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_work_experience_user_id",
                table: "work_experience",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "education");

            migrationBuilder.DropTable(
                name: "language");

            migrationBuilder.DropTable(
                name: "other_information");

            migrationBuilder.DropTable(
                name: "refresh_token");

            migrationBuilder.DropTable(
                name: "user_data");

            migrationBuilder.DropTable(
                name: "user_presentation");

            migrationBuilder.DropTable(
                name: "work_experience");

            migrationBuilder.DropTable(
                name: "user_base");
        }
    }
}