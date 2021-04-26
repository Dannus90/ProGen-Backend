using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Domain.DbModels;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;
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
        
        public async Task<CreateWorkExperienceViewModel> CreateWorkExperience
            (string userId, WorkExperienceDto workExperienceDto)
        {
            var userPresentation = _mapper.Map<WorkExperience>(workExperienceDto);

            await _workExperienceRepository.CreateWorkExperience
                (userPresentation, userId);

            return new CreateWorkExperienceViewModel()
            {
                workExperienceId = Guid.Parse(userId)
            };
        }
        
        public async Task<WorkExperiencesViewModel> GetWorkExperiences
            (string userId)
        {
            var workExperiences = await _workExperienceRepository.GetWorkExperiences
                (userId);
            
            var listWorkExperiencesDto = _mapper.Map<List<WorkExperienceDto>>(workExperiences);

            return new WorkExperiencesViewModel()
            {
                WorkExperienceDto = listWorkExperiencesDto
            };
        }
        
        public async Task<WorkExperienceViewModel> GetWorkExperience
            (string workExperienceId)
        {
            var workExperience = await _workExperienceRepository.GetWorkExperience
                (workExperienceId);
            
            var listWorkExperiencesDto = _mapper.Map<WorkExperienceDto>(workExperience);

            return new WorkExperienceViewModel()
            {
                WorkExperienceDto = listWorkExperiencesDto
            };
        }
    }
}