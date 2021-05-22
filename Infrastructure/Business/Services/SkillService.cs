using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
            var skillId = await _skillRepository.CreateSkill
                (skillDto.SkillName);

            return new CreateSkillViewModel()
            {
                SkillId = skillId
            };
        }
        
        public async Task<SkillsViewModel> GetSkillsBySearchQuery
            (string searchQuery)
        {
            var skills = await _skillRepository.GetSkillsBySearchQuery
                (searchQuery);
            
            var skillsDtos = _mapper.Map<List<SkillDto>>(skills);

            return new SkillsViewModel()
            {
                Skills = skillsDtos
            };
        }
        
        public async Task DeleteSkillById(string skillId)
        {
            await _skillRepository.DeleteSkillById
                (skillId);
        }
    }
}