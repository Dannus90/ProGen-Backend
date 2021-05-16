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
                    zip_code = table.Column<string>(type: "CHAR(128)", nullable: true),
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
                values: new object[] { "6002e474-49f5-4739-bff8-88bf44591a87", "testuser@gmail.com", "Daniel", null, "Persson", "$2a$10$lmiYrmWUDf7klCsGo0VP.uI9DcK.5fUy2Ld34ahg8lQnIanlzThcy" });

            migrationBuilder.InsertData(
                table: "education",
                columns: new[] { "id", "city_en", "city_sv", "country_en", "country_sv", "date_ended", "date_started", "description_en", "description_sv", "education_name_en", "education_name_sv", "exam_name_en", "exam_name_sv", "grade", "subject_area_en", "subject_area_sv", "user_id" },
                values: new object[,]
                {
                    { "c0947531-8752-4cb1-935f-860de6662cb0", "Gothenburg", "Göteborg", "Sweden", "Sverige", new DateTime(2020, 9, 8, 12, 36, 8, 776, DateTimeKind.Local).AddTicks(4790), new DateTime(2020, 4, 11, 12, 36, 8, 771, DateTimeKind.Local).AddTicks(5020), "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ", "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ", "Social Sciences", "Social vetenskap", "Bacheclor with social science", "Kandidatexamen inom socialvetenskap", "VG", "Behavioral science", "Beteendevetenskap", "6002e474-49f5-4739-bff8-88bf44591a87" },
                    { "ef678bc7-cacf-4571-9d32-4863f8f2d26d", "Gothenburg", "Göteborg", "Sweden", "Sverige", new DateTime(2020, 4, 10, 12, 36, 8, 776, DateTimeKind.Local).AddTicks(5290), new DateTime(2019, 9, 24, 12, 36, 8, 776, DateTimeKind.Local).AddTicks(5280), "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ", "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ", "Social Sciences", "Social vetenskap", "Bacheclor with social science", "Kandidatexamen inom socialvetenskap", "VG", "Behavioral science", "Beteendevetenskap", "6002e474-49f5-4739-bff8-88bf44591a87" },
                    { "2c81d739-f485-4424-9cef-f209b6625e91", "Gothenburg", "Göteborg", "Sweden", "Sverige", new DateTime(2019, 9, 23, 12, 36, 8, 776, DateTimeKind.Local).AddTicks(5300), new DateTime(2019, 3, 8, 12, 36, 8, 776, DateTimeKind.Local).AddTicks(5300), "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ", "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ", "Social Sciences", "Social vetenskap", "Bacheclor with social science", "Kandidatexamen inom socialvetenskap", "VG", "Behavioral science", "Beteende vetenskap", "6002e474-49f5-4739-bff8-88bf44591a87" }
                });

            migrationBuilder.InsertData(
                table: "language",
                columns: new[] { "id", "language_en", "language_sv", "user_id" },
                values: new object[,]
                {
                    { "4061a3b7-a94a-4950-b57c-68a2f13ec051", "Swedish", "Svenska", "6002e474-49f5-4739-bff8-88bf44591a87" },
                    { "e70e9be2-97ae-4db3-a873-34b3f32783f0", "English", "Engelska", "6002e474-49f5-4739-bff8-88bf44591a87" },
                    { "758e9e3b-c8e1-4a20-904d-ba8e17616a9c", "Spanish", "Spanska", "6002e474-49f5-4739-bff8-88bf44591a87" },
                    { "7a372210-12ae-4e63-aa7a-aa66a75b8345", "French", "Franska", "6002e474-49f5-4739-bff8-88bf44591a87" }
                });

            migrationBuilder.InsertData(
                table: "other_information",
                columns: new[] { "id", "driving_license_en", "driving_license_sv", "user_id" },
                values: new object[] { "aeb0607d-17e9-4731-a672-cc9e28e413d7", "Driving license B", "Körkort B", "6002e474-49f5-4739-bff8-88bf44591a87" });

            migrationBuilder.InsertData(
                table: "user_data",
                columns: new[] { "id", "city_en", "city_sv", "country_en", "country_sv", "email_cv", "phone_number", "profile_image", "profile_image_public_id", "user_id", "work_title_en", "work_title_sv", "zip_code" },
                values: new object[] { "00b975c9-780a-4a09-96e6-28b6411e9e47", "Gothenburg", "Göteborg", "Sweden", "Sverige", "persson.daniel.1990@gmail.com", "073-3249826", "", null, "6002e474-49f5-4739-bff8-88bf44591a87", "Software developer", "Mjukvaru utvecklare", null });

            migrationBuilder.InsertData(
                table: "user_presentation",
                columns: new[] { "id", "presentation_en", "presentation_sv", "user_id" },
                values: new object[] { "50e7c975-ef10-4da4-a045-e77c1018281d", "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", "6002e474-49f5-4739-bff8-88bf44591a87" });

            migrationBuilder.InsertData(
                table: "work_experience",
                columns: new[] { "id", "city_en", "city_sv", "company_name", "country_en", "country_sv", "date_ended", "date_started", "description_en", "description_sv", "employment_rate", "role_en", "role_sv", "user_id" },
                values: new object[,]
                {
                    { "14f5421c-8229-42a0-a7ca-6b5436a10fe9", "GothenBurg", "Göteborg", "FrontEdge IT", "Sweden", "Sverige", new DateTime(2020, 10, 27, 12, 36, 8, 777, DateTimeKind.Local).AddTicks(890), new DateTime(2020, 4, 11, 12, 36, 8, 777, DateTimeKind.Local).AddTicks(520), "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.", "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.", "FullTime", "Software Developer", "Mjukvaruutvecklare", "6002e474-49f5-4739-bff8-88bf44591a87" },
                    { "c8c351df-1c62-4afa-ad17-e2db3b400ab1", "Gothenburg", "Göteborg", "Stena Line", "Sweden", "Sverige", new DateTime(2021, 3, 27, 12, 36, 8, 777, DateTimeKind.Local).AddTicks(1270), new DateTime(2020, 10, 28, 12, 36, 8, 777, DateTimeKind.Local).AddTicks(1260), "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.", "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.", "PartTime", "Software developer", "Mjukvaruutvecklare", "6002e474-49f5-4739-bff8-88bf44591a87" },
                    { "c00742dd-590c-4373-92c5-edbd09279d1c", "Gothenburg", "Göteborg", "FrontEdge IT", "Sweden", "Sverige", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 3, 28, 12, 36, 8, 777, DateTimeKind.Local).AddTicks(1280), "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.", "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.", "Internship", "Software developer", "Mjukvaruutvecklare", "6002e474-49f5-4739-bff8-88bf44591a87" }
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
