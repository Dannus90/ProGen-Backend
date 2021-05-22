using AutoMapper;
using Core.Domain.DbModels;
using Core.Domain.Dtos;
using Core.Domain.Models;

namespace Core.Mapping
{
    public class SkillsAndUserSkillsMapper : Profile
    {
        public SkillsAndUserSkillsMapper()
        {
            // From UserSkill -> UserSkillModel.
            CreateMap<UserSkill, UserSkillModel>()
                .ForMember(x => x.Id, act =>
                    act.MapFrom(src => src.Id.ToString()))
                .ForMember(x => x.SkillId, act =>
                    act.MapFrom(src => src.SkillId.ToString()))
                .ForMember(x => x.SkillLevel, act =>
                    act.MapFrom(src => src.SkillLevel))
                .ForMember(x => x.UserId, act =>
                    act.MapFrom(src => src.UserId.ToString()));

            CreateMap<Skill, SkillModel>()
                .ForMember(x => x.Id, act =>
                    act.MapFrom(src => src.Id.ToString()))
                .ForMember(x => x.SkillName, act =>
                    act.MapFrom(src => src.SkillName));

            CreateMap<UserSkillAndSkillModel, UserSkillAndSkillDto>()
                .ForMember(x => x.SkillModel, act =>
                    act.MapFrom(src => src.Skill))
                .ForMember(x => x.UserSkillModel, act =>
                    act.MapFrom(src => src.UserSkill));
        }
    }
}