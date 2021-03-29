using System;
using System.Threading.Tasks;
using Core.Domain.DbModels;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task DeleteUserByUserId(Guid userId);
        Task<User> GetUserByUserId(string userId);
    }
}