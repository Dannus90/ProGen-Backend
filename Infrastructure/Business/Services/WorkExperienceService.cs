using System.Threading.Tasks;
using AutoMapper;
using Core.Domain.DbModels;
using Core.Domain.Dtos;
using Infrastructure.Business.Services.Interfaces;
using Infrastructure.Persistence.Repositories.Interfaces;

namespace Infrastructure.Identity.Services
{
    public class WorkExperienceService : IWorkExperienceService
    {
        private readonly IMapper _mapper;
        private readonly IWorkExperienceRepository _workExperienceRepository;

        public WorkExperienceService(IWorkExperienceRepository workExperienceRepository,
            IMapper mapper)
        {
            _workExperienceRepository = workExperienceRepository;
            _mapper = mapper;
        }
        
        public async Task CreateWorkExperience
            (string userId, WorkExperienceDto workExperienceDto)
        {
            var userPresentation = _mapper.Map<WorkExperience>(workExperienceDto);

            await _workExperienceRepository.CreateWorkExperience
                (userPresentation, userId);
        }
    }
}