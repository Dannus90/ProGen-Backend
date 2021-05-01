﻿// <auto-generated />
using System;
using Core.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210501153252_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("EducationName")
                        .IsRequired()
                        .HasColumnType("Char(108)")
                        .HasColumnName("education_name");

                    b.Property<string>("ExamName")
                        .IsRequired()
                        .HasColumnType("Char(108)")
                        .HasColumnName("exam_name");

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
                            Id = "7cb30bd2-07e8-40e0-bcca-1bd8975b388d",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "testuser@gmail.com",
                            FirstName = "John",
                            LastName = "Doe",
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

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("user_data");

                    b.HasData(
                        new
                        {
                            Id = "ef3a69bf-7717-40ec-b747-3b8e18cfcf1f",
                            CityEn = "Gothenburg",
                            CitySv = "Göteborg",
                            CountryEn = "Sweden",
                            CountrySv = "Sverige",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmailCv = "persson.daniel.1990@gmail.com",
                            PhoneNumber = "073-3249826",
                            ProfileImage = "",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "7cb30bd2-07e8-40e0-bcca-1bd8975b388d"
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
                            Id = "70c23906-b9dc-4536-8d16-49b3bbb0287b",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PresentationEn = "PresentationText En",
                            PresentationSv = "PresentationText Sv",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "7cb30bd2-07e8-40e0-bcca-1bd8975b388d"
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
                });

            modelBuilder.Entity("Core.Domain.DbModels.Education", b =>
                {
                    b.HasOne("Core.Domain.DbModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
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