using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "skill",
                columns: table => new
                {
                    id = table.Column<string>(type: "CHAR(36)", nullable: false),
                    skill_name = table.Column<string>(type: "CHAR(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skill", x => x.id);
                });

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
                name: "certificate",
                columns: table => new
                {
                    id = table.Column<string>(type: "CHAR(36)", nullable: false),
                    user_id = table.Column<string>(type: "Char(36)", nullable: false),
                    certificate_name_sv = table.Column<string>(type: "Char(108)", nullable: false),
                    certificate_name_en = table.Column<string>(type: "Char(108)", nullable: false),
                    organisation = table.Column<string>(type: "Char(108)", nullable: false),
                    identification_id = table.Column<string>(type: "Char(108)", nullable: false),
                    reference_address = table.Column<string>(type: "Char(108)", nullable: false),
                    date_issued = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_certificate", x => x.id);
                    table.ForeignKey(
                        name: "FK_certificate_user_base_user_id",
                        column: x => x.user_id,
                        principalTable: "user_base",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                    address_zip_code = table.Column<string>(type: "CHAR(256)", nullable: true),
                    work_title_sv = table.Column<string>(type: "CHAR(128)", nullable: true),
                    work_title_en = table.Column<string>(type: "CHAR(128)", nullable: true),
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
                name: "user_skill",
                columns: table => new
                {
                    id = table.Column<string>(type: "CHAR(36)", nullable: false),
                    user_id = table.Column<string>(type: "Char(36)", nullable: false),
                    skill_id = table.Column<string>(type: "Char(36)", nullable: false),
                    level = table.Column<short>(type: "SMALLINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_skill", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_skill_skill_skill_id",
                        column: x => x.skill_id,
                        principalTable: "skill",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_skill_user_base_user_id",
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

            migrationBuilder.CreateIndex(
                name: "IX_certificate_user_id",
                table: "certificate",
                column: "user_id");

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
                name: "IX_skill_skill_name",
                table: "skill",
                column: "skill_name");

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
                name: "IX_user_skill_skill_id",
                table: "user_skill",
                column: "skill_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_skill_user_id",
                table: "user_skill",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_experience_user_id",
                table: "work_experience",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "certificate");

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
                name: "user_skill");

            migrationBuilder.DropTable(
                name: "work_experience");

            migrationBuilder.DropTable(
                name: "skill");

            migrationBuilder.DropTable(
                name: "user_base");
        }
    }
}
