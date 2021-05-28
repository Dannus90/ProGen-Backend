using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Exceptions;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;
using Infrastructure.Business.Services.Interfaces;
using Infrastructure.Persistence.Repositories.Interfaces;

namespace Infrastructure.Identity.Services
{
    public class SkillService : ISkillService
    {
        private readonly IMapper _mapper;
        private readonly ISkillRepository _skillRepository;

        public SkillService(ISkillRepository skillRepository,
            IMapper mapper)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }

        public async Task<CreateSkillViewModel> CreateSkill
            (SkillDto skillDto)
        {
            if (skillDto.SkillName.Length > 50 || skillDto.SkillName.Length < 2)
                throw new HttpExceptionResponse((int )HttpStatusCode.BadRequest,
                    "Invalid length. Max 50 and min 2 is accepted.");
            
            var skillId = await _skillRepository.CreateSkill
                (skillDto.SkillName);

            return new CreateSkillViewModel()
            {
                SkillId = skillId
            };
        }
        
        public async Task<SkillsViewModel> GetAllSkills
            ()
        {
            var skills = await _skillRepository.GetAllSkills
                ();
            
            var skillsDtos = _mapper.Map<List<SkillDto>>(skills);

            return new SkillsViewModel()
            {
                SkillDtos = skillsDtos
            };
        }
        
        public async Task<SkillViewModel> GetSkillBySkillId
            (string skillId)
        {
            var skill = await _skillRepository.GetSkillBySkillId
                (skillId);

            if (skill == null)
                throw new HttpExceptionResponse
                    ((int) HttpStatusCode.NotFound, "No skill with the provided id was found.");
            
            var skillDto = _mapper.Map<SkillDto>(skill);

            return new SkillViewModel
            {
                SkillDto = skillDto
            };
        }
        
        public async Task DeleteSkillById(string skillId)
        {
            await _skillRepository.DeleteSkillById
                (skillId);
        }
    }
}