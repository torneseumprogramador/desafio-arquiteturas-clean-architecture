using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.InterfacesRepository
{
    public interface IOrderProductRepository
    {
        Task<OrderProduct?> GetByIdAsync(Guid orderId, Guid productId);
        Task<IEnumerable<OrderProduct>> GetByOrderIdAsync(Guid orderId);
        Task AddAsync(OrderProduct orderProduct);
        Task UpdateAsync(OrderProduct orderProduct);
        Task DeleteAsync(Guid orderId, Guid productId);
    }
} 