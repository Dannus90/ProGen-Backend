using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Exceptions;
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
        
        public async Task<CreateUpdateWorkExperienceViewModel> CreateWorkExperience
            (string userId, WorkExperienceDto workExperienceDto)
        {
            var workExperience = _mapper.Map<WorkExperience>(workExperienceDto);

            var workExperienceId = await _workExperienceRepository.CreateWorkExperience
                (workExperience, userId);

            return new CreateUpdateWorkExperienceViewModel()
            {
                WorkExperienceId = workExperienceId
            };
        }
        
        public async Task<WorkExperienceViewModel> UpdateWorkExperience
            (string workExperienceId, WorkExperienceDto workExperienceDto)
        {
            var workExperience = _mapper.Map<WorkExperience>(workExperienceDto);

            var retrievedWorkExperience = await _workExperienceRepository.UpdateWorkExperience
                (workExperienceId, workExperience);
            
            var workExperienceDtoMapped = _mapper.Map<WorkExperienceDto>(retrievedWorkExperience);

            return new WorkExperienceViewModel()
            {
                WorkExperienceDto = workExperienceDtoMapped
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
            
            if (workExperience == null) 
                throw new HttpExceptionResponse((int) HttpStatusCode.NotFound, "Not found");
            
            var singleWorkExperiencesDto = _mapper.Map<WorkExperienceDto>(workExperience);

            return new WorkExperienceViewModel()
            {
                WorkExperienceDto = singleWorkExperiencesDto
            };
        }
        
        public async Task DeleteWorkExperience
            (string workExperienceId)
        {
           await _workExperienceRepository.DeleteWorkExperience(workExperienceId);
        }
    }
}