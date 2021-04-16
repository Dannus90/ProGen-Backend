using System;
using System.Threading.Tasks;
using Core.Domain.DbModels;
using Core.Domain.Models;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task DeleteUserByUserId(Guid userId);
        Task<User> GetUserByUserId(string userId);
        Task<FullnameModel> UpdateUserName(string firstName, string lastName, string userId);
    }
}