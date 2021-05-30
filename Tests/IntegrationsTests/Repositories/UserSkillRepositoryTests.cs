using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.DbModels;
using Core.Domain.Models;
using Infrastructure.Identity.Repositories;
using Infrastructure.Identity.Repositories.Interfaces;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Interfaces;
using Infrastructure.Security;
using NUnit.Framework;

namespace Tests.IntegrationsTests.Repositories
{
    
    public class UserSkillRepositoryTests
    {
        private string setupEmail;
        private string setupPassword;
        private string firstName;
        private string lastName;
        private Guid setupUserId;
        private List<Skill> skills;
        private List<Guid> skillIds;
        private List<Guid> userSkillIds;
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IUserSkillRepository _userSkillRepository;

        public UserSkillRepositoryTests()
        {
            _userAuthRepository = new UserAuthRepository
                (TestConfig.getTestConnectionString());
            _userRepository = new UserRepository
                (TestConfig.getTestConnectionString());
            _skillRepository = new SkillRepository
            (TestConfig.getTestConnectionString());
            _userSkillRepository = new UserSkillRepository
                (TestConfig.getTestConnectionString());
        }
        
        [OneTimeSetUp]
        public async Task Setup()
        {
            setupPassword = "dfgjwasgasgqwqer";
            setupEmail = "farbrorAnka25@gmail.com";
            firstName = "Daniel";
            lastName = "Persson";
            
            var hashedPassword = PasswordHandler.HashPassword(setupPassword);
            var userCredentialsWithName = new UserCredentialsWithName()
            {
                Email = setupEmail,
                Password = hashedPassword,
                FirstName = firstName,
                LastName = lastName
            };
            
            await _userAuthRepository.RegisterUser(userCredentialsWithName);
            var user = await _userRepository.GetUserByEmail(setupEmail);

            setupUserId = user.Id;
            
            // Arrange
            skills = new List<Skill>
            {
                new()
                {
                    SkillName = "TestSkill1",
                },
                new()
                {
                    SkillName = "TestSkill2"
                },
                new()
                {
                    SkillName = "TestSkill3"
                }
            };

            var skillIdFirst = await _skillRepository.CreateSkill(skills[0].SkillName);
            var skillIdSecondary = await _skillRepository.CreateSkill(skills[1].SkillName);
            var skillIdTertiary = await _skillRepository.CreateSkill(skills[2].SkillName);
        
            skillIds = new List<Guid>();
        
            skillIds.AddRange(new List<Guid>
            { skillIdFirst, skillIdSecondary, skillIdTertiary });
            
            userSkillIds = new List<Guid>();

            var userSkillIdFirst = await _userSkillRepository
                .CreateUserSkill(skillIdFirst, 3, setupUserId.ToString());
            var userSkillIdSecondary = await _userSkillRepository
                .CreateUserSkill(skillIdSecondary, 4, setupUserId.ToString());
            var userSkillIdTertiary = await _userSkillRepository
                .CreateUserSkill(skillIdTertiary, 5, setupUserId.ToString());
            
            userSkillIds.AddRange(new List<Guid>
                { userSkillIdFirst, userSkillIdSecondary, userSkillIdTertiary });
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await _userRepository.DeleteUserByUserId(setupUserId);
            foreach (var userSkillId in userSkillIds)
            {
                await _userSkillRepository.
                    DeleteUserSkill(setupUserId.ToString(), userSkillId.ToString());
            }
            foreach (var skillId in skillIds)
            {
                await _skillRepository.DeleteSkillById(skillId.ToString());
            }
        }
        
        [Test]
        public async Task 
            CreateAndDeleteUserSkill_WithSkillData_SuccessfullyCreatesAndDeletesUserSkill()
        {
            // Arrange
            const string skillName = "TestSkillForTests";
            
            // Act
            var skillId = await _skillRepository.CreateSkill(skillName);
            var retrievedSkill = await _skillRepository.GetSkillBySkillId(skillId.ToString());
            var retrievedUserSkillId = await _userSkillRepository
                .CreateUserSkill(skillId, 4, setupUserId.ToString());
            var retrievedUserSkill =
                await _userSkillRepository.
                    GetSingleUserSkill(setupUserId.ToString(), retrievedUserSkillId.ToString());

            // Assert
            Assert.IsNotNull(skillId);
            Assert.IsNotNull(retrievedUserSkillId);
            Assert.IsNotNull(retrievedSkill);
            Assert.AreEqual(skillName, retrievedSkill.SkillName.Trim());
            Assert.AreEqual(4, retrievedUserSkill.UserSkill.SkillLevel);
            Assert.AreEqual(skillName, retrievedUserSkill.Skill.SkillName.Trim());
            
            // Clean up
            var userSkillIdAfterDelete = await _userSkillRepository.DeleteUserSkill
                (setupUserId.ToString(), retrievedUserSkillId.ToString().Trim());
            await _skillRepository.DeleteSkillById
                (skillId.ToString());
            
            // Assert clean up
            Assert.IsNull(await _skillRepository.GetSkillBySkillId
                (skillId.ToString()));

            var userSkill = await _userSkillRepository.GetSingleUserSkill
                (setupUserId.ToString(), userSkillIdAfterDelete.ToString().Trim());
            
            Assert.IsNull(userSkill.Skill.SkillName);
        }
        
        [Test]
        public async Task 
            GetAllUserSkills_ByUserId_SuccessfullyGetsAllUserSkillsByUserId()
        {
            // Act
            var userSkills = await _userSkillRepository.GetAllUserSkills(setupUserId.ToString());
            
            // Assert
            Assert.IsTrue(userSkills.Count() >= 3);
        }
        
        [Test]
        public async Task 
            UpdatesUserSkill_ByUserIdWithSkillLevel_SuccessfullyUpdatesUserSkill()
        {
            // Act
            var retrievedUserSkillId = await _userSkillRepository.UpdateUserSkill
                (userSkillIds[0].ToString(), 5);
            var userSkill = await _userSkillRepository.GetSingleUserSkill
                (setupUserId.ToString(), retrievedUserSkillId.ToString());
            
            // Assert
            Assert.AreEqual(userSkill.UserSkill.SkillLevel, 5);
        }
    }
}