using System;
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

        public async Task<CreateUpdateDeleteUserSkillViewModel> CreateUserSkill
            (UserSkillDto userSkillDto, string userId)
        {
            var userSkillId = await _userSkillRepository.CreateUserSkill
                (userSkillDto.SkillId, userSkillDto.SkillLevel, userId);

            return new CreateUpdateDeleteUserSkillViewModel
            {
                UserSkillId = userSkillId
            };
        }
        
        public async Task<UserSkillsViewModel> GetAllUserSkills
            (string userId)
        {
            var userSkillAndSkillModels = await _userSkillRepository.GetAllUserSkills
                (userId);

            var userSkillAndSkillDtos = _mapper.Map<List<UserSkillAndSkillDto>>(userSkillAndSkillModels);

            return new UserSkillsViewModel()
            {
                UserSkillAndSkillDtos = userSkillAndSkillDtos
            };
        }
        
        public async Task<UserSkillViewModel> GetSingleUserSkill
            (string userId, string userSkillId)
        {
            var userSkillAndSkillModel = await _userSkillRepository.GetSingleUserSkill
                (userId, userSkillId);

            if (Guid.Empty == userSkillAndSkillModel.UserSkill.Id)
                throw new HttpExceptionResponse((int) HttpStatusCode.NotFound, "Not Found");

            var userSkillAndSkillDto = _mapper.Map<UserSkillAndSkillDto>(userSkillAndSkillModel);

            return new UserSkillViewModel
            {
                UserSkillAndSkillDto = userSkillAndSkillDto
            };
        }
        
        public async Task<CreateUpdateDeleteUserSkillViewModel> UpdateUserSkill
            (string userSkillId, UserSkillDto userSkillDto)
        {
            var userSkillIdAfterUpdate = await _userSkillRepository.UpdateUserSkill
                (userSkillId, userSkillDto.SkillLevel);
            
            return new CreateUpdateDeleteUserSkillViewModel
            {
                UserSkillId = userSkillIdAfterUpdate
            };
        }
        
        public async Task<CreateUpdateDeleteUserSkillViewModel> DeleteUserSkill
            (string userId, string userSkillId)
        {
            var userSkillIdAfterDelete = await _userSkillRepository.DeleteUserSkill
                (userId, userSkillId);
            
            return new CreateUpdateDeleteUserSkillViewModel
            {
                UserSkillId = userSkillIdAfterDelete
            };
        }
    }
}