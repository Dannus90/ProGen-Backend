using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.DbModels;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Interfaces;
using NUnit.Framework;

namespace Tests.IntegrationsTests.Repositories
{
    
    public class SkillRepositoryTests
    {
        private List<Skill> skills;
        private List<Guid> skillIds;
        private readonly ISkillRepository _skillRepository;


        public SkillRepositoryTests()
        {
            _skillRepository = new SkillRepository
                (TestConfig.getTestConnectionString());
        }
        
        [OneTimeSetUp]
        public async Task Setup()
        {
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

        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            foreach (var skillId in skillIds)
            {
                await _skillRepository.DeleteSkillById(skillId.ToString());
            }
        }
        
        [Test]
        public async Task 
            CreateAndDeleteSkill_WithSkillData_SuccessfullyCreatesAndDeletesSkill()
        {
            // Arrange
            const string skillName = "TestSkillForTests";
            
            // Act
            var skillId = await _skillRepository.CreateSkill(skillName);
            var retrievedSkill = await _skillRepository.GetSkillBySkillId(skillId.ToString());

            // Assert
            Assert.IsNotNull(skillId);
            Assert.IsNotNull(retrievedSkill);
            Assert.AreEqual(skillName, retrievedSkill.SkillName.Trim());
            
            // Clean up
            await _skillRepository.DeleteSkillById(skillId.ToString());
            
            // Assert clean up
            Assert.IsNull(await _skillRepository.GetSkillBySkillId(skillId.ToString()));
        }
        
        [Test]
        public async Task 
            GetAllSkills_SuccessfullyGetsAllSkills()
        {
            // Act
            var skills = await _skillRepository
                .GetAllSkills();

            // Assert
            Assert.IsNotNull(skills);
            Assert.IsTrue(skills.Count() >= 3);
        }
    }
}
