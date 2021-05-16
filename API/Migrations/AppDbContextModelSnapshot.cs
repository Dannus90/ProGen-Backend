﻿// <auto-generated />
using System;
using Core.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "6.0.0-preview.2.21154.2")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Core.Domain.DbModels.Education", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("id");

                    b.Property<string>("CityEn")
                        .IsRequired()
                        .HasColumnType("Char(72)")
                        .HasColumnName("city_en");

                    b.Property<string>("CitySv")
                        .IsRequired()
                        .HasColumnType("Char(72)")
                        .HasColumnName("city_sv");

                    b.Property<string>("CountryEn")
                        .IsRequired()
                        .HasColumnType("Char(72)")
                        .HasColumnName("country_en");

                    b.Property<string>("CountrySv")
                        .IsRequired()
                        .HasColumnType("Char(72)")
                        .HasColumnName("country_sv");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime>("DateEnded")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_ended")
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime>("DateStarted")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_started")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("DescriptionEn")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("description_en");

                    b.Property<string>("DescriptionSv")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("description_sv");

                    b.Property<string>("EducationNameEn")
                        .IsRequired()
                        .HasColumnType("Char(108)")
                        .HasColumnName("education_name_en");

                    b.Property<string>("EducationNameSv")
                        .IsRequired()
                        .HasColumnType("Char(108)")
                        .HasColumnName("education_name_sv");

                    b.Property<string>("ExamNameEn")
                        .IsRequired()
                        .HasColumnType("Char(108)")
                        .HasColumnName("exam_name_en");

                    b.Property<string>("ExamNameSv")
                        .IsRequired()
                        .HasColumnType("Char(108)")
                        .HasColumnName("exam_name_sv");

                    b.Property<string>("Grade")
                        .IsRequired()
                        .HasColumnType("Char(72)")
                        .HasColumnName("grade");

                    b.Property<string>("SubjectAreaEn")
                        .IsRequired()
                        .HasColumnType("Char(108)")
                        .HasColumnName("subject_area_en");

                    b.Property<string>("SubjectAreaSv")
                        .IsRequired()
                        .HasColumnType("Char(108)")
                        .HasColumnName("subject_area_sv");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("Char(36)")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("education");

                    b.HasData(
                        new
                        {
                            Id = "1fbd6902-d754-4904-ac54-194631d9dce2",
                            CityEn = "Gothenburg",
                            CitySv = "Göteborg",
                            CountryEn = "Sweden",
                            CountrySv = "Sverige",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateEnded = new DateTime(2020, 9, 8, 13, 39, 35, 834, DateTimeKind.Local).AddTicks(7350),
                            DateStarted = new DateTime(2020, 4, 11, 13, 39, 35, 829, DateTimeKind.Local).AddTicks(5240),
                            DescriptionEn = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ",
                            DescriptionSv = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ",
                            EducationNameEn = "Social Sciences",
                            EducationNameSv = "Social vetenskap",
                            ExamNameEn = "Bacheclor with social science",
                            ExamNameSv = "Kandidatexamen inom socialvetenskap",
                            Grade = "VG",
                            SubjectAreaEn = "Behavioral science",
                            SubjectAreaSv = "Beteendevetenskap",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "c823eaa0-ed84-49fb-91f0-767c875c1ed2"
                        },
                        new
                        {
                            Id = "6826bce6-ab73-4883-8a01-0547d45978ff",
                            CityEn = "Gothenburg",
                            CitySv = "Göteborg",
                            CountryEn = "Sweden",
                            CountrySv = "Sverige",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateEnded = new DateTime(2020, 4, 10, 13, 39, 35, 834, DateTimeKind.Local).AddTicks(8000),
                            DateStarted = new DateTime(2019, 9, 24, 13, 39, 35, 834, DateTimeKind.Local).AddTicks(7980),
                            DescriptionEn = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ",
                            DescriptionSv = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ",
                            EducationNameEn = "Social Sciences",
                            EducationNameSv = "Social vetenskap",
                            ExamNameEn = "Bacheclor with social science",
                            ExamNameSv = "Kandidatexamen inom socialvetenskap",
                            Grade = "VG",
                            SubjectAreaEn = "Behavioral science",
                            SubjectAreaSv = "Beteendevetenskap",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "c823eaa0-ed84-49fb-91f0-767c875c1ed2"
                        },
                        new
                        {
                            Id = "b72fbcab-3a29-4953-b020-a2508f5dcc12",
                            CityEn = "Gothenburg",
                            CitySv = "Göteborg",
                            CountryEn = "Sweden",
                            CountrySv = "Sverige",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateEnded = new DateTime(2019, 9, 23, 13, 39, 35, 834, DateTimeKind.Local).AddTicks(8010),
                            DateStarted = new DateTime(2019, 3, 8, 13, 39, 35, 834, DateTimeKind.Local).AddTicks(8010),
                            DescriptionEn = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ",
                            DescriptionSv = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. ",
                            EducationNameEn = "Social Sciences",
                            EducationNameSv = "Social vetenskap",
                            ExamNameEn = "Bacheclor with social science",
                            ExamNameSv = "Kandidatexamen inom socialvetenskap",
                            Grade = "VG",
                            SubjectAreaEn = "Behavioral science",
                            SubjectAreaSv = "Beteende vetenskap",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "c823eaa0-ed84-49fb-91f0-767c875c1ed2"
                        });
                });

            modelBuilder.Entity("Core.Domain.DbModels.Language", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("id");

                    b.Property<string>("LanguageEn")
                        .IsRequired()
                        .HasColumnType("Char(128)")
                        .HasColumnName("language_en");

                    b.Property<string>("LanguageSv")
                        .IsRequired()
                        .HasColumnType("Char(128)")
                        .HasColumnName("language_sv");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("Char(36)")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("language");

                    b.HasData(
                        new
                        {
                            Id = "cf7f9284-b710-4fe2-94e3-00241e4b13ff",
                            LanguageEn = "Swedish",
                            LanguageSv = "Svenska",
                            UserId = "c823eaa0-ed84-49fb-91f0-767c875c1ed2"
                        },
                        new
                        {
                            Id = "af681f95-c649-4cf5-9533-76f859af6307",
                            LanguageEn = "English",
                            LanguageSv = "Engelska",
                            UserId = "c823eaa0-ed84-49fb-91f0-767c875c1ed2"
                        },
                        new
                        {
                            Id = "a03e44f9-8bc6-4bcf-8b80-8c0a2e474b62",
                            LanguageEn = "Spanish",
                            LanguageSv = "Spanska",
                            UserId = "c823eaa0-ed84-49fb-91f0-767c875c1ed2"
                        },
                        new
                        {
                            Id = "56b7eb7b-f195-4e22-84c2-d5a035f64d04",
                            LanguageEn = "French",
                            LanguageSv = "Franska",
                            UserId = "c823eaa0-ed84-49fb-91f0-767c875c1ed2"
                        });
                });

            modelBuilder.Entity("Core.Domain.DbModels.OtherInformation", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("DrivingLicenseEn")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("driving_license_en");

                    b.Property<string>("DrivingLicenseSv")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("driving_license_sv");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("Char(36)")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("other_information");

                    b.HasData(
                        new
                        {
                            Id = "66a4385e-c2b7-454c-98e2-f2dd2818f6dc",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DrivingLicenseEn = "Driving license B",
                            DrivingLicenseSv = "Körkort B",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "c823eaa0-ed84-49fb-91f0-767c875c1ed2"
                        });
                });

            modelBuilder.Entity("Core.Domain.DbModels.RefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("id");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("refresh_token");

                    b.Property<DateTime>("TokenSetAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("token_set_at")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("Char(36)")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("Token");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("refresh_token");
                });

            modelBuilder.Entity("Core.Domain.DbModels.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("CHAR(128)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("CHAR(128)")
                        .HasColumnName("first_name");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_login");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("CHAR(128)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("CHAR(500)")
                        .HasColumnName("password");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("user_base");

                    b.HasData(
                        new
                        {
                            Id = "c823eaa0-ed84-49fb-91f0-767c875c1ed2",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "testuser@gmail.com",
                            FirstName = "Daniel",
                            LastName = "Persson",
                            Password = "$2a$10$lmiYrmWUDf7klCsGo0VP.uI9DcK.5fUy2Ld34ahg8lQnIanlzThcy",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Core.Domain.DbModels.UserData", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("id");

                    b.Property<string>("AddressZipCode")
                        .HasColumnType("CHAR(256)")
                        .HasColumnName("address_zip_code");

                    b.Property<string>("CityEn")
                        .HasColumnType("CHAR(128)")
                        .HasColumnName("city_en");

                    b.Property<string>("CitySv")
                        .HasColumnType("CHAR(128)")
                        .HasColumnName("city_sv");

                    b.Property<string>("CountryEn")
                        .HasColumnType("CHAR(128)")
                        .HasColumnName("country_en");

                    b.Property<string>("CountrySv")
                        .HasColumnType("CHAR(128)")
                        .HasColumnName("country_sv");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("EmailCv")
                        .HasColumnType("CHAR(128)")
                        .HasColumnName("email_cv");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("CHAR(64)")
                        .HasColumnName("phone_number");

                    b.Property<string>("ProfileImage")
                        .HasColumnType("CHAR(256)")
                        .HasColumnName("profile_image");

                    b.Property<string>("ProfileImagePublicId")
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("profile_image_public_id");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("Char(36)")
                        .HasColumnName("user_id");

                    b.Property<string>("WorkTitleEn")
                        .HasColumnType("CHAR(128)")
                        .HasColumnName("work_title_en");

                    b.Property<string>("WorkTitleSv")
                        .HasColumnType("CHAR(128)")
                        .HasColumnName("work_title_sv");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("user_data");

                    b.HasData(
                        new
                        {
                            Id = "a4b39dc9-5411-4550-9b8b-14f16b28e756",
                            CityEn = "Gothenburg",
                            CitySv = "Göteborg",
                            CountryEn = "Sweden",
                            CountrySv = "Sverige",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmailCv = "persson.daniel.1990@gmail.com",
                            PhoneNumber = "073-3249826",
                            ProfileImage = "",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "c823eaa0-ed84-49fb-91f0-767c875c1ed2",
                            WorkTitleEn = "Software developer",
                            WorkTitleSv = "Mjukvaru utvecklare"
                        });
                });

            modelBuilder.Entity("Core.Domain.DbModels.UserPresentation", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("PresentationEn")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("presentation_en");

                    b.Property<string>("PresentationSv")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("presentation_sv");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("Char(36)")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("user_presentation");

                    b.HasData(
                        new
                        {
                            Id = "6c6d7e84-53a7-4c8e-9fbc-ac480e90a18c",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PresentationEn = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                            PresentationSv = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "c823eaa0-ed84-49fb-91f0-767c875c1ed2"
                        });
                });

            modelBuilder.Entity("Core.Domain.DbModels.WorkExperience", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("CHAR(36)")
                        .HasColumnName("id");

                    b.Property<string>("CityEn")
                        .IsRequired()
                        .HasColumnType("Char(72)")
                        .HasColumnName("city_en");

                    b.Property<string>("CitySv")
                        .IsRequired()
                        .HasColumnType("Char(72)")
                        .HasColumnName("city_sv");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("Char(54)")
                        .HasColumnName("company_name");

                    b.Property<string>("CountryEn")
                        .IsRequired()
                        .HasColumnType("Char(72)")
                        .HasColumnName("country_en");

                    b.Property<string>("CountrySv")
                        .IsRequired()
                        .HasColumnType("Char(72)")
                        .HasColumnName("country_sv");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime>("DateEnded")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_ended")
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime>("DateStarted")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_started")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("DescriptionEn")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("description_en");

                    b.Property<string>("DescriptionSv")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("description_sv");

                    b.Property<string>("EmploymentRate")
                        .IsRequired()
                        .HasColumnType("Char(54)")
                        .HasColumnName("employment_rate");

                    b.Property<string>("RoleEn")
                        .IsRequired()
                        .HasColumnType("Char(108)")
                        .HasColumnName("role_en");

                    b.Property<string>("RoleSv")
                        .IsRequired()
                        .HasColumnType("Char(108)")
                        .HasColumnName("role_sv");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("Char(36)")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("work_experience");

                    b.HasData(
                        new
                        {
                            Id = "bb8f84b0-d7ce-427f-bdcb-8a3b00e094d6",
                            CityEn = "GothenBurg",
                            CitySv = "Göteborg",
                            CompanyName = "FrontEdge IT",
                            CountryEn = "Sweden",
                            CountrySv = "Sverige",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateEnded = new DateTime(2020, 10, 27, 13, 39, 35, 835, DateTimeKind.Local).AddTicks(5070),
                            DateStarted = new DateTime(2020, 4, 11, 13, 39, 35, 835, DateTimeKind.Local).AddTicks(4630),
                            DescriptionEn = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.",
                            DescriptionSv = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.",
                            EmploymentRate = "FullTime",
                            RoleEn = "Software Developer",
                            RoleSv = "Mjukvaruutvecklare",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "c823eaa0-ed84-49fb-91f0-767c875c1ed2"
                        },
                        new
                        {
                            Id = "8dac4a45-f12e-4876-b8b5-7437b5854f34",
                            CityEn = "Gothenburg",
                            CitySv = "Göteborg",
                            CompanyName = "Stena Line",
                            CountryEn = "Sweden",
                            CountrySv = "Sverige",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateEnded = new DateTime(2021, 3, 27, 13, 39, 35, 835, DateTimeKind.Local).AddTicks(5530),
                            DateStarted = new DateTime(2020, 10, 28, 13, 39, 35, 835, DateTimeKind.Local).AddTicks(5520),
                            DescriptionEn = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.",
                            DescriptionSv = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.",
                            EmploymentRate = "PartTime",
                            RoleEn = "Software developer",
                            RoleSv = "Mjukvaruutvecklare",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "c823eaa0-ed84-49fb-91f0-767c875c1ed2"
                        },
                        new
                        {
                            Id = "7b2119f2-6cdb-4da0-aa38-e32f69863573",
                            CityEn = "Gothenburg",
                            CitySv = "Göteborg",
                            CompanyName = "FrontEdge IT",
                            CountryEn = "Sweden",
                            CountrySv = "Sverige",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateEnded = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateStarted = new DateTime(2021, 3, 28, 13, 39, 35, 835, DateTimeKind.Local).AddTicks(5540),
                            DescriptionEn = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.",
                            DescriptionSv = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.",
                            EmploymentRate = "Internship",
                            RoleEn = "Software developer",
                            RoleSv = "Mjukvaruutvecklare",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "c823eaa0-ed84-49fb-91f0-767c875c1ed2"
                        });
                });

            modelBuilder.Entity("Core.Domain.DbModels.Education", b =>
                {
                    b.HasOne("Core.Domain.DbModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Domain.DbModels.Language", b =>
                {
                    b.HasOne("Core.Domain.DbModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Domain.DbModels.OtherInformation", b =>
                {
                    b.HasOne("Core.Domain.DbModels.User", null)
                        .WithOne()
                        .HasForeignKey("Core.Domain.DbModels.OtherInformation", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Domain.DbModels.RefreshToken", b =>
                {
                    b.HasOne("Core.Domain.DbModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Domain.DbModels.UserData", b =>
                {
                    b.HasOne("Core.Domain.DbModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Domain.DbModels.UserPresentation", b =>
                {
                    b.HasOne("Core.Domain.DbModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Domain.DbModels.WorkExperience", b =>
                {
                    b.HasOne("Core.Domain.DbModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
