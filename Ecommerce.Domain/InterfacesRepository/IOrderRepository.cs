using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.InterfacesRepository
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Guid id);
    }
} 