using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class InitialMigration : Migration
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
                values: new object[] { "c823eaa0-ed84-49fb-91f0-767c875c1ed2", "testuser@gmail.com", "Daniel", null, "Persson", "$2a$10$lmiYrmWUDf7klCsGo0VP.uI9DcK.5fUy2Ld34ahg8lQnIanlzThcy" });

            migrationBuilder.InsertData(
                table: "education",
                columns: new[] { "id", "city_en", "city_sv", "country_en", "country_sv", "date_ended", "date_started", "description_en", "description_sv", "education_name_en", "education_name_sv", "exam_name_en", "exam_name_sv", "grade", "subject_area_en", "subject_area_sv", "user_id" },
                values: new object[,]
                {
                    { "1fbd6902-d754-4904-ac54-194631d9dce2", "Gothenburg", "Göteborg", "Sweden", "Sverige", new DateTime(2020, 9, 8, 13, 39, 35, 834, DateTimeKind.Local).AddTicks(7350), new DateTime(2020, 4, 11, 13, 39, 35, 829, DateTimeKind.Local).AddTicks(5240), "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ", "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ", "Social Sciences", "Social vetenskap", "Bacheclor with social science", "Kandidatexamen inom socialvetenskap", "VG", "Behavioral science", "Beteendevetenskap", "c823eaa0-ed84-49fb-91f0-767c875c1ed2" },
                    { "6826bce6-ab73-4883-8a01-0547d45978ff", "Gothenburg", "Göteborg", "Sweden", "Sverige", new DateTime(2020, 4, 10, 13, 39, 35, 834, DateTimeKind.Local).AddTicks(8000), new DateTime(2019, 9, 24, 13, 39, 35, 834, DateTimeKind.Local).AddTicks(7980), "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ", "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ", "Social Sciences", "Social vetenskap", "Bacheclor with social science", "Kandidatexamen inom socialvetenskap", "VG", "Behavioral science", "Beteendevetenskap", "c823eaa0-ed84-49fb-91f0-767c875c1ed2" },
                    { "b72fbcab-3a29-4953-b020-a2508f5dcc12", "Gothenburg", "Göteborg", "Sweden", "Sverige", new DateTime(2019, 9, 23, 13, 39, 35, 834, DateTimeKind.Local).AddTicks(8010), new DateTime(2019, 3, 8, 13, 39, 35, 834, DateTimeKind.Local).AddTicks(8010), "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ", "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ", "Social Sciences", "Social vetenskap", "Bacheclor with social science", "Kandidatexamen inom socialvetenskap", "VG", "Behavioral science", "Beteende vetenskap", "c823eaa0-ed84-49fb-91f0-767c875c1ed2" }
                });

            migrationBuilder.InsertData(
                table: "language",
                columns: new[] { "id", "language_en", "language_sv", "user_id" },
                values: new object[,]
                {
                    { "cf7f9284-b710-4fe2-94e3-00241e4b13ff", "Swedish", "Svenska", "c823eaa0-ed84-49fb-91f0-767c875c1ed2" },
                    { "af681f95-c649-4cf5-9533-76f859af6307", "English", "Engelska", "c823eaa0-ed84-49fb-91f0-767c875c1ed2" },
                    { "a03e44f9-8bc6-4bcf-8b80-8c0a2e474b62", "Spanish", "Spanska", "c823eaa0-ed84-49fb-91f0-767c875c1ed2" },
                    { "56b7eb7b-f195-4e22-84c2-d5a035f64d04", "French", "Franska", "c823eaa0-ed84-49fb-91f0-767c875c1ed2" }
                });

            migrationBuilder.InsertData(
                table: "other_information",
                columns: new[] { "id", "driving_license_en", "driving_license_sv", "user_id" },
                values: new object[] { "66a4385e-c2b7-454c-98e2-f2dd2818f6dc", "Driving license B", "Körkort B", "c823eaa0-ed84-49fb-91f0-767c875c1ed2" });

            migrationBuilder.InsertData(
                table: "user_data",
                columns: new[] { "id", "address_zip_code", "city_en", "city_sv", "country_en", "country_sv", "email_cv", "phone_number", "profile_image", "profile_image_public_id", "user_id", "work_title_en", "work_title_sv" },
                values: new object[] { "a4b39dc9-5411-4550-9b8b-14f16b28e756", null, "Gothenburg", "Göteborg", "Sweden", "Sverige", "persson.daniel.1990@gmail.com", "073-3249826", "", null, "c823eaa0-ed84-49fb-91f0-767c875c1ed2", "Software developer", "Mjukvaru utvecklare" });

            migrationBuilder.InsertData(
                table: "user_presentation",
                columns: new[] { "id", "presentation_en", "presentation_sv", "user_id" },
                values: new object[] { "6c6d7e84-53a7-4c8e-9fbc-ac480e90a18c", "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", "c823eaa0-ed84-49fb-91f0-767c875c1ed2" });

            migrationBuilder.InsertData(
                table: "work_experience",
                columns: new[] { "id", "city_en", "city_sv", "company_name", "country_en", "country_sv", "date_ended", "date_started", "description_en", "description_sv", "employment_rate", "role_en", "role_sv", "user_id" },
                values: new object[,]
                {
                    { "bb8f84b0-d7ce-427f-bdcb-8a3b00e094d6", "GothenBurg", "Göteborg", "FrontEdge IT", "Sweden", "Sverige", new DateTime(2020, 10, 27, 13, 39, 35, 835, DateTimeKind.Local).AddTicks(5070), new DateTime(2020, 4, 11, 13, 39, 35, 835, DateTimeKind.Local).AddTicks(4630), "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.", "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.", "FullTime", "Software Developer", "Mjukvaruutvecklare", "c823eaa0-ed84-49fb-91f0-767c875c1ed2" },
                    { "8dac4a45-f12e-4876-b8b5-7437b5854f34", "Gothenburg", "Göteborg", "Stena Line", "Sweden", "Sverige", new DateTime(2021, 3, 27, 13, 39, 35, 835, DateTimeKind.Local).AddTicks(5530), new DateTime(2020, 10, 28, 13, 39, 35, 835, DateTimeKind.Local).AddTicks(5520), "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.", "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.", "PartTime", "Software developer", "Mjukvaruutvecklare", "c823eaa0-ed84-49fb-91f0-767c875c1ed2" },
                    { "7b2119f2-6cdb-4da0-aa38-e32f69863573", "Gothenburg", "Göteborg", "FrontEdge IT", "Sweden", "Sverige", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 3, 28, 13, 39, 35, 835, DateTimeKind.Local).AddTicks(5540), "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.", "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.", "Internship", "Software developer", "Mjukvaruutvecklare", "c823eaa0-ed84-49fb-91f0-767c875c1ed2" }
                });

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
