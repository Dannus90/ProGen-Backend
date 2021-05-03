using AutoMapper;
using Core.Domain.DbModels;
using Core.Domain.Dtos;
using Core.Domain.Models;
using Core.Domain.ViewModels;

namespace Core.Mapping
{
    public class SimpleMappers : Profile
    {
        public SimpleMappers()
        {
            // From UserCredentialsDto -> UserCredentials.
            CreateMap<UserCredentialsDto, UserCredentials>().ReverseMap();
            
            // From UserCredentialsDto -> UserCredentials.
            CreateMap<UserCredentialsWithNameDto, UserCredentialsWithName>().ReverseMap();

            // From UserCredentialsDto -> UserCredentials -> UserCredentialsDto.
            CreateMap<TokenDataDto, TokenData>().ReverseMap();
            
            // From FullUserInformation -> FullUserInformationDto -> FullUserInformation.
            CreateMap<FullUserInformation, FullUserInformationDto>().ReverseMap();
            
            // From UserDataDto -> UserDataModel -> UserDataDto.
            CreateMap<UserDataDto, UserDataModel>().ReverseMap();
            
            // From UserData -> UserData.
            CreateMap<UserData, UserDataDto>();
            
            // From ProfileImageModel -> UserImageViewModel.
            CreateMap<ProfileImageModel, UserImageViewModel>();
            
            // From ChangePasswordModel -> ChangePasswordDto -> ChangePasswordModel.
            CreateMap<ChangePasswordModel, ChangePasswordDto>().ReverseMap();
            
            // From ChangeEmailModel -> ChangeEmailDto -> ChangeEmailModel.
            CreateMap<ChangeEmailModel, ChangeEmailDto>().ReverseMap();
            
            // From UserPresentation -> UserPresentationDto -> UserPresentation.
            CreateMap<UserPresentation, UserPresentationDto>().ReverseMap();
            
            // From WorkExperienceDto -> WorkExperience -> WorkExperienceDto.
            CreateMap<WorkExperienceDto, WorkExperience>().ReverseMap();
            
            // From WorkExperienceDto -> WorkExperience -> WorkExperienceDto.
            CreateMap<EducationDto, Education>().ReverseMap();
            
            // From OtherInformationDto -> OtherInformation -> OtherInformationDto.
            CreateMap<OtherInformationDto, OtherInformation>().ReverseMap();
        }
    }
}