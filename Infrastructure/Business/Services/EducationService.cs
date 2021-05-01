using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Exceptions;
using Core.Domain.DbModels;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;
using Infrastructure.Business.Services.Interfaces;
using Infrastructure.Persistence.Repositories.Interfaces;
using Npgsql;

namespace Infrastructure.Identity.Services
{
    public class EducationService : IEducationService
    {
        private readonly IMapper _mapper;
        private readonly IEducationRepository _educationRepository;

        public EducationService(IEducationRepository educationRepository,
            IMapper mapper)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
        }

        public async Task<CreateUpdateEducationViewModel> CreateEducation(string userId, EducationDto educationDto)
        {
            var education = _mapper.Map<Education>(educationDto);
            
            var educationId = await _educationRepository.CreateEducation
                (education, userId);

            return new CreateUpdateEducationViewModel()
            {
                EducationId = educationId
            };
        }

        public async Task<EducationViewModel> GetEducation(string educationId)
        {
            var education = await _educationRepository.GetEducation
                (educationId);

            if (education == null) throw new HttpExceptionResponse(404, "Not found");
            
            var singleEducationDto = _mapper.Map<EducationDto>(education);
            
            return new EducationViewModel()
            {
                EducationDto = singleEducationDto
            };
        }
        
        public async Task<EducationsViewModel> GetEducations(string userId)
        {
            var educations = await _educationRepository.GetEducations
                (userId);

            var listEducationsDto = _mapper.Map<List<EducationDto>>(educations);
            
            return new EducationsViewModel()
            {
                EducationsDto = listEducationsDto
            };
        }
    }
}