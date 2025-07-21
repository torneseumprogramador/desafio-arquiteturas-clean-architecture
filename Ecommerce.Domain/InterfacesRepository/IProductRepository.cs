using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.InterfacesRepository
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
} 