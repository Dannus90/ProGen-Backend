using System.Threading.Tasks;
using API.helpers;
using AutoMapper;
using Core.Application.Exceptions;
using Core.Domain.Dtos;
using Core.Domain.Models;
using Core.Domain.ViewModels;
using Infrastructure.Business.Services.Interfaces;
using Infrastructure.Persistence.Repositories.Interfaces;
using Infrastructure.Security;

namespace Infrastructure.Identity.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly IMapper _mapper;
        private readonly IUserDataRepository _userDataRepository;
        private readonly IUserRepository _userRepository;

        public UserDataService(IUserDataRepository userDataRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userDataRepository = userDataRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserInformationViewModel> GetFullUserData(string userId)
        {
            var fullUserInformation = await _userDataRepository.GetFullUserInformation(userId);

            if (fullUserInformation == null) throw new HttpExceptionResponse(404, "No userdata was found");
            
            var fullUserInformationDto = _mapper.Map<FullUserInformationDto>(fullUserInformation);

            return new UserInformationViewModel()
            {
                FullUserInformationDto = fullUserInformationDto
            };
        }
    }
}