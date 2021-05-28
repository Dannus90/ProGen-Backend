using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Domain.DbModels;
using Core.Domain.Dtos;
using Core.Domain.Models;
using Core.Domain.ViewModels;
using Dapper;
using Infrastructure.Persistence.Repositories.Interfaces;
using Npgsql;

namespace Infrastructure.Persistence
{
    public class FullCvInformationRepository : IFullCvInformationRepository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;
        public FullCvInformationRepository(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }

        public async Task<FullCvInformationViewModel> GetFullCvInformation(string userId)
        {
            const string fullUserInformationQuery = @"
                    SELECT user_base.id AS IdString,
                            user_base.email AS Email,
                            user_base.first_name AS Firstname,
                            user_base.last_name AS Lastname,
                            user_base.last_login AS LastLogin,
                            user_base.created_at AS CreatedAt,
                            user_base.updated_at AS UpdatedAt,
                            user_data.id AS IdString,
                            user_data.user_id AS UserIdString,
                            user_data.phone_number AS PhoneNumber,
                            user_data.email_cv AS EmailCv,
                            user_data.city_sv AS CitySv,
                            user_data.city_en AS CityEn,
                            user_data.country_sv AS CountrySv,
                            user_data.country_en AS CountryEn,
                            user_data.address_zip_code AS AddressZipCode,
                            user_data.profile_image AS ProfileImage,
                            user_data.profile_image_public_id AS ProfileImagePublicId,
                            user_data.work_title_sv AS WorkTitleSv,
                            user_data.work_title_en AS WorkTitleEn,
                            user_data.created_at AS CreatedAt,
                            user_data.updated_at AS UpdatedAt
                    FROM user_base
                        LEFT JOIN user_data ON user_base.id = user_data.user_id
                    WHERE user_base.id = @UserId;
                ";
            
            const string workExperiencesQuery = @"
                   SELECT work_experience.id AS IdString,
                            work_experience.user_id AS UserIdString,
                            work_experience.employment_rate AS EmploymentRate,
                            work_experience.company_name AS CompanyName,
                            work_experience.role_sv AS RoleSv,
                            work_experience.role_en AS RoleEn,
                            work_experience.description_sv AS DescriptionSv,
                            work_experience.description_en AS DescriptionEn,
                            work_experience.city_sv AS CitySv,
                            work_experience.city_en AS CityEn,
                            work_experience.country_sv AS CountrySv,
                            work_experience.country_en AS CountryEn,
                            work_experience.date_started AS DateStarted,
                            work_experience.date_ended AS DateEnded,
                            work_experience.created_at AS CreatedAt,
                            work_experience.updated_at AS UpdatedAt
                   FROM work_experience WHERE user_id = @UserId;
                ";
            
            const string educationsQuery = @"
                   SELECT id AS IdString,
                            user_id AS UserIdString,
                            education_name_sv AS EducationNameSv,
                            education_name_en AS EducationNameEn,
                            exam_name_sv AS ExamNameSv,
                            exam_name_en AS ExamNameEn,
                            subject_area_sv AS SubjectAreaSv,
                            subject_area_en AS SubjectAreaEn,
                            description_sv AS DescriptionSv,
                            description_en AS DescriptionEn,
                            grade AS Grade,
                            city_sv AS CitySv,
                            city_en AS CityEn,
                            country_sv AS CountrySv,
                            country_en AS CountryEn,
                            date_started AS DateStarted,
                            date_ended AS DateEnded,
                            created_at AS CreatedAt,
                            updated_at AS UpdatedAt
                   FROM education WHERE user_id = @UserId;
                ";
            
            const string languagesQuery = @"
                   SELECT id AS IdString,
                        user_id AS UserIdString,
                        language_sv AS LanguageSv,
                        language_en AS LanguageEn
                   FROM language
                   WHERE user_id = @UserId;
                ";
            
            const string otherInformationQuery = @"
                   SELECT id AS IdString,
                        user_id AS UserIdString,
                        driving_license_sv AS DrivingLicenseSv,
                        driving_license_en AS DrivingLicenseEn,
                        created_at AS CreatedAt,
                        updated_at AS UpdatedAt
                   FROM other_information
                   WHERE user_id = @UserId;
                ";
            
            const string userPresentationQuery = @"
                    SELECT id AS IdString,
                        user_Id AS UserIdString,
                        presentation_sv AS PresentationSv,
                        presentation_en AS PresentationEn,
                        created_at AS CreatedAt,
                        updated_at AS UpdatedAt
                    FROM user_presentation WHERE user_id = @UserId;  
                ";
            
            const string userSkillAndSkillQuery = @"
                   SELECT user_skill.id AS IdString,
                            user_skill.skill_id AS SkillIdString,
                            user_skill.user_id AS UserIdString,
                            user_skill.skill_level AS SkillLevel,
                            skill.id AS IdString,
                            skill.skill_name AS SkillName
                    FROM user_skill
                    INNER JOIN skill ON user_skill.skill_id = skill.id
                    WHERE user_id = @UserId
                ";
            
            const string certificatesQuery = @"
                   SELECT id AS IdString,
                            user_id AS UserIdString,
                            certificate_name_sv AS CertificateNameSv,
                            certificate_name_en AS CertificateNameEn,
                            organisation AS Organisation,
                            identification_id AS IdentificationId,
                            reference_address AS ReferenceAddress,
                            date_issued AS DateIssued,
                            created_at AS CreatedAt,
                            updated_at AS UpdatedAt
                    FROM certificate WHERE user_id = @UserId;
                ";
            
            var fullCvInformationViewModel = new FullCvInformationViewModel();

            using var conn = await connectDb(_connectionString);
            
            // TRANSACTION STARTS.
            using var transaction = conn.BeginTransaction();
            
            var fullUserInformations = await conn.QueryAsync<User, UserData, FullUserInformation>
            (fullUserInformationQuery, (user, userData) => new FullUserInformation()
                {
                    User = user,
                    UserData = userData
                },
                new
                {
                    UserId = userId
                }, splitOn: "IdString");

            var fullUserInformation =
                fullUserInformations.Any() ? fullUserInformations.ToList()[0] : new FullUserInformation();
                

            // SET FULL USER INFORMATION
            fullCvInformationViewModel.FullUserInformationDto = _mapper.Map<FullUserInformationDto>
                (fullUserInformation);
            
            var workexperiences = await conn.QueryAsync<WorkExperience>
            (workExperiencesQuery, new
                {
                    UserId = userId
                });
            
            // SET WORK EXPERIENCES
            fullCvInformationViewModel.WorkExperienceDtos = _mapper.Map<List<WorkExperienceDto>>
                (workexperiences);
            
            var educations = await conn.QueryAsync<Education>
            (educationsQuery, new
            {
                UserId = userId
            });

            fullCvInformationViewModel.EducationDtos = _mapper.Map<List<EducationDto>>(educations);

            var languages = await conn.QueryAsync<Language>(languagesQuery, new
            {
                UserId = userId
            });
            
            // SET LANGUAGES
            fullCvInformationViewModel.LanguageDtos = _mapper.Map<List<LanguageDto>>(languages);
            
            var otherInformation = await conn.QueryFirstOrDefaultAsync<OtherInformation>
            (otherInformationQuery, new
            {
                UserId = userId
            });
            
            // SET OTHER INFORMATION
            fullCvInformationViewModel.OtherInformationDto = _mapper.Map<OtherInformationDto>(otherInformation);

            var userPresentation = await conn.QueryFirstOrDefaultAsync<UserPresentation>
            (userPresentationQuery, new
            {
                UserId = userId
            });
            
            // SET USER PRESENTATION
            fullCvInformationViewModel.UserPresentationDto = _mapper
                .Map<UserPresentationDto>(userPresentation);
            
            var userSkillAndSkillDtos = await
                conn.QueryAsync<UserSkill, Skill, UserSkillAndSkillModel>
                (userSkillAndSkillQuery, (userSkill, Skill) => new UserSkillAndSkillModel
                    {
                        UserSkill = userSkill,
                        Skill = Skill
                    }, new
                    {
                        UserId = userId
                    },
                    splitOn: "IdString");
            
            // SET USER SKILL AND USER SKILL DTOS
            fullCvInformationViewModel.UserSkillAndSkillDtos = _mapper
                .Map<List<UserSkillAndSkillDto>>(userSkillAndSkillDtos);
            
            var certificates = await conn.QueryAsync<Certificate>(certificatesQuery, new
            {
                UserId = userId
            });
            
            fullCvInformationViewModel.CertificateDtos = _mapper
                .Map<List<CertificateDto>>(certificates);

            transaction.Commit();

            return fullCvInformationViewModel;
        }
        
        private static async Task<IDbConnection> connectDb(string connectionString)
        {
            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}