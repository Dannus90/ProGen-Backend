using System.Data;
using System.Threading.Tasks;
using AutoMapper;
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
    }
}