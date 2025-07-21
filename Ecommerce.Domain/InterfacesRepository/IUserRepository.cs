using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.InterfacesRepository
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
    }
} 