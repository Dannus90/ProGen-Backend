using System.Threading.Tasks;
using AutoMapper;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;
using Infrastructure.Business.Services.Interfaces;
using Infrastructure.Persistence.Repositories.Interfaces;

namespace Infrastructure.Identity.Services
{
    public class UserSkillService : IUserSkillService
    {
        private readonly IMapper _mapper;
        private readonly IUserSkillRepository _userSkillRepository;

        public UserSkillService(IUserSkillRepository userSkillRepository,
            IMapper mapper)
        {
            _userSkillRepository = userSkillRepository;
            _mapper = mapper;
        }

        public async Task<CreateUpdateUserSkillViewModel> CreateUserSkill
            (UserSkillDto userSkillDto, string userId)
        {
            var userSkillId = await _userSkillRepository.CreateUserSkill
                (userSkillDto.SkillId, userSkillDto.SkillLevel, userId);

            return new CreateUpdateUserSkillViewModel()
            {
                UserSkillId = userSkillId
            };
        }
    }
}