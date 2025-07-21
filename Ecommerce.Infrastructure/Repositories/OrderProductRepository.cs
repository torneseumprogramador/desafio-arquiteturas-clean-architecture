using Ecommerce.Domain.Entities;
using Ecommerce.Domain.InterfacesRepository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly EcommerceDbContext _context;
        public OrderProductRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OrderProduct orderProduct)
        {
            await _context.OrderProducts.AddAsync(orderProduct);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid orderId, Guid productId)
        {
            var op = await _context.OrderProducts.FindAsync(orderId, productId);
            if (op != null)
            {
                _context.OrderProducts.Remove(op);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<OrderProduct> GetByIdAsync(Guid orderId, Guid productId)
        {
            return await _context.OrderProducts.FindAsync(orderId, productId);
        }

        public async Task<IEnumerable<OrderProduct>> GetByOrderIdAsync(Guid orderId)
        {
            return await _context.OrderProducts.Where(op => op.OrderId == orderId).ToListAsync();
        }

        public async Task UpdateAsync(OrderProduct orderProduct)
        {
            _context.OrderProducts.Update(orderProduct);
            await _context.SaveChangesAsync();
        }
    }
} 