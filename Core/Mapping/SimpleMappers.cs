using AutoMapper;
using Core.Domain.Dtos;
using Core.Domain.Models;

namespace Core.Mapping
{
    public class SimpleMappers : Profile
    {
        public SimpleMappers()
        {
            // From UserCredentialsDto -> UserCredentials.
            CreateMap<UserCredentialsDto, UserCredentials>();
        }
    }
}