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
    
    public class CertificateRepositoryTests
    {
        private string setupEmail;
        private string setupPassword;
        private string firstName;
        private string lastName;
        private Certificate certificate;
        private Guid setupUserId;
        private List<Certificate> educations;
        private List<Guid> educationIds;
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICertificateRepository _certificateRepository;

        public CertificateRepositoryTests()
        {
            _userAuthRepository = new UserAuthRepository
                (TestConfig.getTestConnectionString());
            _userRepository = new UserRepository
                (TestConfig.getTestConnectionString());
            _certificateRepository = new CertificateRepository(
                    TestConfig.getTestConnectionString());
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
            certificate = new Certificate()
            {
                UserId = setupUserId,
                Organisation = "TestOrganisation",
                IdentificationId = "sgwgjqjq34tjtq34jt+regnafbas",
                DateIssued = DateTime.Now,
                ReferenceAddress = "Test address 34",
                CertificateNameEn = "Super certificate",
                CertificateNameSv = "Super certifikatet"
            };

            educations = new List<Certificate>()
            {
                new()
                {
                    UserId = setupUserId,
                    Organisation = "TestOrganisation",
                    IdentificationId = "sgwgjqjq34tjtq34jt+regnafbas",
                    DateIssued = DateTime.Now,
                    ReferenceAddress = "Test address 34",
                    CertificateNameEn = "Super certificate",
                    CertificateNameSv = "Super certifikatet"
                },
                new()
                {
                    UserId = setupUserId,
                    Organisation = "TestOrganisation",
                    IdentificationId = "sgwgjqjq34tjtq34jt+regnafbas",
                    DateIssued = DateTime.Now,
                    ReferenceAddress = "Test address 34",
                    CertificateNameEn = "Super certificate",
                    CertificateNameSv = "Super certifikatet"
                },
                new()
                {
                    UserId = setupUserId,
                    Organisation = "TestOrganisation",
                    IdentificationId = "sgwgjqjq34tjtq34jt+regnafbas",
                    DateIssued = DateTime.Now,
                    ReferenceAddress = "Test address 34",
                    CertificateNameEn = "Super certificate",
                    CertificateNameSv = "Super certifikatet"
                }
            };
            
            var educationIdFirst = await _certificateRepository.CreateCertificate
                (educations[0], setupUserId.ToString());
            var educationIdSecondary = await _certificateRepository.CreateCertificate
                (educations[1], setupUserId.ToString());
            var educationIdTertiary = await _certificateRepository.CreateCertificate
                (educations[2], setupUserId.ToString());

            educationIds = new List<Guid>();
            
            educationIds.AddRange(new List<Guid> 
                { educationIdFirst, educationIdSecondary, educationIdTertiary });
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await _userRepository.DeleteUserByUserId(setupUserId);
            foreach (var educationId in educationIds)
            {
                await _certificateRepository.DeleteSingleCertificateForUser
                    (educationId.ToString(), setupUserId.ToString());
            }
        }
        
        [Test]
        public async Task 
            CreateAndDeleteCertificate_WithEducationData_SuccessfullyCreatesAndDeletesCertificate()
        {
            // Act
            var certificateId = await _certificateRepository.CreateCertificate
                (certificate, setupUserId.ToString());

            var retrievedCertificate = 
                await _certificateRepository.GetCertificateForUser
                    (certificateId.ToString(), setupUserId.ToString());

            // Assert
            Assert.IsNotNull(retrievedCertificate);
            Assert.AreEqual(retrievedCertificate.Organisation.Trim(), certificate.Organisation);
            Assert.AreEqual(retrievedCertificate.ReferenceAddress.Trim(), certificate.ReferenceAddress);
            
            // Clean up
            await _certificateRepository.DeleteSingleCertificateForUser
                (certificateId.ToString(), setupUserId.ToString());
            
            var retrievedCertificateAfterDelete = 
                await _certificateRepository.GetCertificateForUser
                    (certificateId.ToString(), setupUserId.ToString());
            
            Assert.IsNull(retrievedCertificateAfterDelete);
        }
        
        [Test]
        public async Task 
            CreateAndUpdateCertificate_WithNewData_SuccessfullyCreatesAndUpdateCertificate()
        {
            // Act
            var certificateId = await _certificateRepository.CreateCertificate
                (certificate, setupUserId.ToString());
            
            // Arrange
            var certificateForUpdate = new Certificate()
            {
                UserId = setupUserId,
                Organisation = "TestOrganisation2",
                IdentificationId = "sgwgjqjq34tjtq34jt+regnafbas2",
                DateIssued = DateTime.Now,
                ReferenceAddress = "Test address 342",
                CertificateNameEn = "Super certificate2",
                CertificateNameSv = "Super certifikatet2"
            };
            
            // Act
            var updatedCertificate = 
                await _certificateRepository.UpdateCertificateForUser
                    (certificateId.ToString(), certificateForUpdate, setupUserId.ToString());

            // Assert
            Assert.IsNotNull(updatedCertificate);
            Assert.AreEqual(updatedCertificate.Organisation.Trim(),
                certificateForUpdate.Organisation);
            Assert.AreEqual(updatedCertificate.IdentificationId.Trim(),
                certificateForUpdate.IdentificationId);
            
            // Clean up
            await _certificateRepository.DeleteSingleCertificateForUser
                (certificateId.ToString(), setupUserId.ToString());
            
            var retrievedCertificateAfterDelete = 
                await _certificateRepository.GetCertificateForUser
                    (certificateId.ToString(), setupUserId.ToString());
            
            Assert.IsNull(retrievedCertificateAfterDelete);
        }
        
        [Test]
        public async Task 
            GetAllEducations_ByUserId_SuccessfullyGetsAllEducation()
        {
            // Act
            var certificates = await _certificateRepository
                .GetAllCertificatesForUser(setupUserId.ToString());

            // Assert
            Assert.IsNotNull(certificates);
            Assert.AreEqual(certificates.Count(), 3);
        }
    }
}
