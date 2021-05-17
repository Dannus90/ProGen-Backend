using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Exceptions;
using Core.Domain.DbModels;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;
using Infrastructure.Identity.Repositories.Interfaces;
using Infrastructure.Identity.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Identity.Services
{
    public class UserPresentationService : IUserPresentationService
    {
        private readonly IMapper _mapper;
        private readonly IUserPresentationRepository _userPresentationRepository;

        public UserPresentationService(IUserPresentationRepository userPresentationRepository,
            IMapper mapper)
        {
            _userPresentationRepository = userPresentationRepository;
            _mapper = mapper;
        }
        
        public async Task<UserPresentationViewModel> UpdateUserPresentation
            (string userId, UserPresentationDto userPresentationDto)
        {
            var userPresentation = _mapper.Map<UserPresentation>(userPresentationDto);

            var retrievedUserPresentation = await _userPresentationRepository.UpdateUserPresentation
                (userId, userPresentation);
            
            var retrievedUserPresentationDto = _mapper.Map<UserPresentationDto>(retrievedUserPresentation);

            return new UserPresentationViewModel()
            {
                UserPresentationData = retrievedUserPresentationDto
            };
        }
        
        public async Task<UserPresentationViewModel> GetUserPresentation(string userId)
        {
            var retrievedUserPresentation = await _userPresentationRepository.GetUserPresentation(userId);

            if (retrievedUserPresentation == null)
            {
                throw new HttpExceptionResponse(StatusCodes.Status404NotFound, "No user presentation exist yet");
            }
            
            var retrievedUserPresentationDto = _mapper.Map<UserPresentationDto>(retrievedUserPresentation);

            return new UserPresentationViewModel()
            {
                UserPresentationData = retrievedUserPresentationDto
            };
        }
    }
}
        
