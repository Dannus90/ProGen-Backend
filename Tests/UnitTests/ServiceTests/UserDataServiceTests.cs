using System;
using System.Collections.Generic;
using API.helpers.Cloudinary.Interfaces;
using AutoMapper;
using Core.Application.Exceptions;
using Core.Domain.Models;
using Core.Mapping;
using Infrastructure.Identity.Services;
using Infrastructure.Persistence.Repositories.Interfaces;
using Moq;
using NUnit.Framework;
using Tests.UnitTests.ServiceTests.UserDataServiceMock;

namespace Tests.UnitTests.ServiceTests
{
    public class UserDataServiceTests
    {
        [TestCase]
        public void GetFullUserData_GetsFullUserDataSuccessfully()
        {
            // Arrange
            var userDataRepositoryMock = new Mock<IUserDataRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var cloudinaryHelperMock = new Mock<ICloudinaryHelper>();
            var userId = Guid.NewGuid().ToString();
            var returnMock = UserDataServiceMockMethods.GetFullUserInformation(userId);

            var mappers = new List<Profile>
            {
                new SimpleMappers(),
                new SkillsAndUserSkillsMapper()
            };

            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfiles(mappers); });

            var mapper = mapperConfig.CreateMapper();

            userDataRepositoryMock.Setup(x => x.GetFullUserInformation(userId)).ReturnsAsync(returnMock);

            var userDataService = new UserDataService
            (userDataRepositoryMock.Object,
                userRepositoryMock.Object,
                mapper,
                cloudinaryHelperMock.Object);

            // Act
            var userInformationViewModel = userDataService
                .GetFullUserData(userId).Result;

            // Assert
            Assert.IsNotNull
                (userInformationViewModel);
            Assert.AreEqual
            (userInformationViewModel.FullUserInformationDto.User.Email,
                "persson.daniel.1990@gmail.com");
        }

        [TestCase]
        public void GetFullUserData_FailsToRetrieveUserDataAndExceptionIsThrown()
        {
            // Arrange
            var userDataRepositoryMock = new Mock<IUserDataRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var cloudinaryHelperMock = new Mock<ICloudinaryHelper>();
            var userId = Guid.NewGuid().ToString();

            var mappers = new List<Profile>
            {
                new SimpleMappers(),
                new SkillsAndUserSkillsMapper()
            };

            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfiles(mappers); });

            var mapper = mapperConfig.CreateMapper();

            userDataRepositoryMock.Setup(x => x.GetFullUserInformation(userId))
                .ReturnsAsync((FullUserInformation) null);

            var userDataService = new UserDataService
            (userDataRepositoryMock.Object,
                userRepositoryMock.Object,
                mapper,
                cloudinaryHelperMock.Object);

            // Assert
            Assert.ThrowsAsync<HttpExceptionResponse>(() => userDataService.GetFullUserData(userId));
        }
    }
}