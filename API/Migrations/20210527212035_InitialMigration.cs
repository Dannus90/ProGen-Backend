using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class InitialMigration : Migration
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
                    skill_level = table.Column<short>(type: "SMALLINT", nullable: false)
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

            migrationBuilder.InsertData(
                table: "skill",
                columns: new[] { "id", "skill_name" },
                values: new object[,]
                {
                    { "ff0ad6f0-df3b-46e5-8930-bf76983fc8b4", "Javascript" },
                    { "3ed64509-7447-4961-a509-5d09581e79eb", "React" },
                    { "145b5b77-3da9-4d08-870a-be26a8a3b338", "C#" },
                    { "d545c804-64b9-47f2-9a14-448318981eb0", "SQL" },
                    { "8092fe80-6e34-426b-8775-2ee15acaa3b2", "CSS" }
                });

            migrationBuilder.InsertData(
                table: "user_base",
                columns: new[] { "id", "email", "first_name", "last_login", "last_name", "password" },
                values: new object[] { "d3b56b95-995c-4d77-906f-c51410f86a2e", "testuser@gmail.com", "Daniel", null, "Persson", "$2a$10$lmiYrmWUDf7klCsGo0VP.uI9DcK.5fUy2Ld34ahg8lQnIanlzThcy" });

            migrationBuilder.InsertData(
                table: "certificate",
                columns: new[] { "id", "certificate_name_en", "certificate_name_sv", "date_issued", "identification_id", "organisation", "reference_address", "user_id" },
                values: new object[,]
                {
                    { "2064cd05-e91c-4577-8bc0-a8ca05a42e1d", "Javascript for beginners", "Javascript för nybörjare", new DateTime(2021, 2, 16, 23, 20, 34, 669, DateTimeKind.Local).AddTicks(6590), "werg0wrg0wuerrg0whrg", "Udemy", "www.google.com", "d3b56b95-995c-4d77-906f-c51410f86a2e" },
                    { "d02768a6-d9a1-4be9-84c8-e2bf83b6af1c", "Css for beginners", "Css för nybörjare", new DateTime(2020, 12, 28, 23, 20, 34, 669, DateTimeKind.Local).AddTicks(7050), "werg0wrg0wuerrg0whrg2", "Udemy", "www.google.com", "d3b56b95-995c-4d77-906f-c51410f86a2e" },
                    { "2a491cbb-27bc-44b5-ac4d-0ae02026af24", "React for beginners", "React för nybörjare", new DateTime(2020, 11, 8, 23, 20, 34, 669, DateTimeKind.Local).AddTicks(7070), "werg0wrg0wuerrg0whrg", "Udemy", "www.google.com", "d3b56b95-995c-4d77-906f-c51410f86a2e" }
                });

            migrationBuilder.InsertData(
                table: "education",
                columns: new[] { "id", "city_en", "city_sv", "country_en", "country_sv", "date_ended", "date_started", "description_en", "description_sv", "education_name_en", "education_name_sv", "exam_name_en", "exam_name_sv", "grade", "subject_area_en", "subject_area_sv", "user_id" },
                values: new object[,]
                {
                    { "8884d084-b372-4751-bdb6-4d7c1d65c0e3", "Gothenburg", "Göteborg", "Sweden", "Sverige", new DateTime(2020, 9, 19, 23, 20, 34, 668, DateTimeKind.Local).AddTicks(1740), new DateTime(2020, 4, 22, 23, 20, 34, 662, DateTimeKind.Local).AddTicks(8010), "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ", "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ", "Social Sciences", "Social vetenskap", "Bacheclor with social science", "Kandidatexamen inom socialvetenskap", "VG", "Behavioral science", "Beteendevetenskap", "d3b56b95-995c-4d77-906f-c51410f86a2e" },
                    { "9fc5909c-9ef0-49e7-846a-ef7136fa7cf6", "Gothenburg", "Göteborg", "Sweden", "Sverige", new DateTime(2020, 4, 21, 23, 20, 34, 668, DateTimeKind.Local).AddTicks(2330), new DateTime(2019, 10, 5, 23, 20, 34, 668, DateTimeKind.Local).AddTicks(2320), "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ", "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ", "Social Sciences", "Social vetenskap", "Bacheclor with social science", "Kandidatexamen inom socialvetenskap", "VG", "Behavioral science", "Beteendevetenskap", "d3b56b95-995c-4d77-906f-c51410f86a2e" },
                    { "d65be854-e869-49eb-8e61-d9a1fa1e4ada", "Gothenburg", "Göteborg", "Sweden", "Sverige", new DateTime(2019, 10, 4, 23, 20, 34, 668, DateTimeKind.Local).AddTicks(2340), new DateTime(2019, 3, 19, 23, 20, 34, 668, DateTimeKind.Local).AddTicks(2340), "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ", "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ", "Social Sciences", "Social vetenskap", "Bacheclor with social science", "Kandidatexamen inom socialvetenskap", "VG", "Behavioral science", "Beteende vetenskap", "d3b56b95-995c-4d77-906f-c51410f86a2e" }
                });

            migrationBuilder.InsertData(
                table: "language",
                columns: new[] { "id", "language_en", "language_sv", "user_id" },
                values: new object[,]
                {
                    { "09ea480a-8b82-4f0c-9772-51483e2c3686", "Swedish", "Svenska", "d3b56b95-995c-4d77-906f-c51410f86a2e" },
                    { "231d53e5-d2c4-4c6a-98a7-0215eea69c49", "English", "Engelska", "d3b56b95-995c-4d77-906f-c51410f86a2e" },
                    { "4a5986e9-7c95-4ea4-9fe7-e8d913a829b9", "Spanish", "Spanska", "d3b56b95-995c-4d77-906f-c51410f86a2e" },
                    { "09bee2a4-eee9-4c32-a2d0-d240896ce464", "French", "Franska", "d3b56b95-995c-4d77-906f-c51410f86a2e" }
                });

            migrationBuilder.InsertData(
                table: "other_information",
                columns: new[] { "id", "driving_license_en", "driving_license_sv", "user_id" },
                values: new object[] { "a4a24ca7-d011-40e0-9f00-c6641cf27688", "Driving license B", "Körkort B", "d3b56b95-995c-4d77-906f-c51410f86a2e" });

            migrationBuilder.InsertData(
                table: "user_data",
                columns: new[] { "id", "address_zip_code", "city_en", "city_sv", "country_en", "country_sv", "email_cv", "phone_number", "profile_image", "profile_image_public_id", "user_id", "work_title_en", "work_title_sv" },
                values: new object[] { "8c5c0b6c-190f-4aec-97f0-f618f9a97115", "Ponnygatan 3, 43131", "Gothenburg", "Göteborg", "Sweden", "Sverige", "persson.daniel.1990@gmail.com", "073-3249826", "", null, "d3b56b95-995c-4d77-906f-c51410f86a2e", "Software developer", "Mjukvaru utvecklare" });

            migrationBuilder.InsertData(
                table: "user_presentation",
                columns: new[] { "id", "presentation_en", "presentation_sv", "user_id" },
                values: new object[] { "d3caeae4-1435-4cf8-8602-d11df03d0cfa", "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", "d3b56b95-995c-4d77-906f-c51410f86a2e" });

            migrationBuilder.InsertData(
                table: "user_skill",
                columns: new[] { "id", "skill_id", "skill_level", "user_id" },
                values: new object[,]
                {
                    { "a8f5fc7b-2ea0-4026-ae41-cc453701e95a", "ff0ad6f0-df3b-46e5-8930-bf76983fc8b4", (short)4, "d3b56b95-995c-4d77-906f-c51410f86a2e" },
                    { "b6efd813-ab29-4478-bb35-4de1f91aa24f", "3ed64509-7447-4961-a509-5d09581e79eb", (short)3, "d3b56b95-995c-4d77-906f-c51410f86a2e" },
                    { "fded5f09-ff13-4652-bbed-91d01ba87faa", "145b5b77-3da9-4d08-870a-be26a8a3b338", (short)2, "d3b56b95-995c-4d77-906f-c51410f86a2e" },
                    { "577d52cd-a9a4-4e78-aaff-17c1c709b617", "d545c804-64b9-47f2-9a14-448318981eb0", (short)2, "d3b56b95-995c-4d77-906f-c51410f86a2e" },
                    { "641e641b-d2ef-4d75-8f27-e5c10a128e31", "8092fe80-6e34-426b-8775-2ee15acaa3b2", (short)5, "d3b56b95-995c-4d77-906f-c51410f86a2e" }
                });

            migrationBuilder.InsertData(
                table: "work_experience",
                columns: new[] { "id", "city_en", "city_sv", "company_name", "country_en", "country_sv", "date_ended", "date_started", "description_en", "description_sv", "employment_rate", "role_en", "role_sv", "user_id" },
                values: new object[,]
                {
                    { "69794072-4bbe-46a6-88ec-4499828d75ab", "GothenBurg", "Göteborg", "FrontEdge IT", "Sweden", "Sverige", new DateTime(2020, 11, 7, 23, 20, 34, 668, DateTimeKind.Local).AddTicks(9240), new DateTime(2020, 4, 22, 23, 20, 34, 668, DateTimeKind.Local).AddTicks(8800), "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.", "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.", "FullTime", "Software Developer", "Mjukvaruutvecklare", "d3b56b95-995c-4d77-906f-c51410f86a2e" },
                    { "6f47bfea-5e88-4a08-9ec7-360e6ce1da8b", "Gothenburg", "Göteborg", "Stena Line", "Sweden", "Sverige", new DateTime(2021, 4, 7, 23, 20, 34, 668, DateTimeKind.Local).AddTicks(9700), new DateTime(2020, 11, 8, 23, 20, 34, 668, DateTimeKind.Local).AddTicks(9690), "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.", "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.", "PartTime", "Software developer", "Mjukvaruutvecklare", "d3b56b95-995c-4d77-906f-c51410f86a2e" },
                    { "27aa91c2-bdc7-4af2-82cc-b5666457ae3c", "Gothenburg", "Göteborg", "FrontEdge IT", "Sweden", "Sverige", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 4, 8, 23, 20, 34, 668, DateTimeKind.Local).AddTicks(9700), "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.", "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.", "Internship", "Software developer", "Mjukvaruutvecklare", "d3b56b95-995c-4d77-906f-c51410f86a2e" }
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
                column: "skill_name",
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
                name: "IX_user_skill_skill_id",
                table: "user_skill",
                column: "skill_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_skill_skill_id_user_id",
                table: "user_skill",
                columns: new[] { "skill_id", "user_id" },
                unique: true);

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
