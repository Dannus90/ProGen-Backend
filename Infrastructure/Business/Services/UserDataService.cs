using System;
using System.Threading.Tasks;
using API.helpers.Cloudinary.Interfaces;
using AutoMapper;
using Core.Application.Exceptions;
using Core.Domain.Dtos;
using Core.Domain.Models;
using Core.Domain.ViewModels;
using Infrastructure.Business.Services.Interfaces;
using Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Identity.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly IMapper _mapper;
        private readonly IUserDataRepository _userDataRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICloudinaryHelper _cloudinaryHelper;

        public UserDataService(IUserDataRepository userDataRepository,
            IUserRepository userRepository,
            IMapper mapper,
            ICloudinaryHelper cloudinaryHelper)
        {
            _userDataRepository = userDataRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _cloudinaryHelper = cloudinaryHelper;
        }

        public async Task<UserInformationViewModel> GetFullUserData(string userId)
        {
            var fullUserInformation = await _userDataRepository.GetFullUserInformation(userId);

            if (fullUserInformation == null) throw new HttpExceptionResponse(404, "No userdata was found");
            
            var fullUserInformationDto = _mapper.Map<FullUserInformationDto>(fullUserInformation);

            return new UserInformationViewModel()
            {
                FullUserInformationDto = fullUserInformationDto
            };
        }
        
        public async Task<UserDataViewModel> UpdateUserData(string userId, UserDataDto userDataDto)
        {
            var userData = _mapper.Map<UserDataModel>(userDataDto);
            
            var retrievedUserData = await _userDataRepository.UpdateUserData(userId, userData);

            if (retrievedUserData == null) throw new HttpExceptionResponse(404, "No userdata was found");

            var retrievedName = await _userRepository.UpdateUserName
                (userData.FirstName, userData.LastName, userId);
            
            if (retrievedName == null) throw new HttpExceptionResponse(404, "No userdata was found");
            
            var retrievedUserDataDto = _mapper.Map<UserDataDto>(retrievedUserData);

            retrievedUserDataDto.FirstName = retrievedName.FirstName;
            retrievedUserDataDto.LastName = retrievedName.LastName;

            return new UserDataViewModel()
            {
                UserDataDto = retrievedUserDataDto
            };
        }

        public async Task<UserImageViewModel> UploadProfileImage(IFormFile file, string userId)
        {
            var userData = await _userDataRepository.GetFullUserInformation(userId);

            var imagePublicId = userData.UserData.ProfileImagePublicId == null 
                ? Guid.NewGuid() 
                : new Guid(userData.UserData.ProfileImagePublicId);

            var publicImageUrl =
                _cloudinaryHelper.UploadImageOrPdfToCloudinary
                (file, userId, imagePublicId.ToString(),
                    "profile-images/");

            var profileImageData = await _userDataRepository.UploadProfileImage
                (imagePublicId.ToString(), publicImageUrl, userId);
            
            
            var userImageViewModel = _mapper.Map<UserImageViewModel>(profileImageData);

            return userImageViewModel;
        }
        
        public async Task DeleteProfileImage(string publicId, string userId)
        { 
            _cloudinaryHelper.DeleteResourceFromCloudinary(userId, publicId, "profile-images/");

            await _userDataRepository.DeleteProfileImage(userId);
        }
    }
}