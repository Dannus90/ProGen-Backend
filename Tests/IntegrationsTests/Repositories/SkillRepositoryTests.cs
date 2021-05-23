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
    
    public class SkillRepositoryTests
    {

        private readonly ISkillRepository _skillRepository;


        public SkillRepositoryTests()
        {
            _skillRepository = new SkillRepository
                (TestConfig.getTestConnectionString());
        }
        
        [OneTimeSetUp]
        public async Task Setup()
        {
                 
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            
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
    }
}
