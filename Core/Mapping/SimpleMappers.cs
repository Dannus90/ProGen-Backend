using AutoMapper;
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
        }
    }
}