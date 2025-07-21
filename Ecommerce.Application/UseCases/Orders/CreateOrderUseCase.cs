using System;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.InterfacesRepository;

namespace Ecommerce.Application.UseCases.Orders
{
    public class CreateOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public CreateOrderUseCase(IOrderRepository orderRepository, IUserRepository userRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task<OrderDto> ExecuteAsync(CreateOrderDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user == null) throw new Exception("Usuário não encontrado");

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow,
                OrderProducts = new List<OrderProduct>()
            };

            decimal total = 0;
            foreach (var item in dto.Products)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null) throw new Exception($"Produto {item.ProductId} não encontrado");
                if (product.Stock < item.Quantity) throw new Exception($"Estoque insuficiente para o produto {product.Name}");
                order.OrderProducts.Add(new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });
                total += product.Price * item.Quantity;
            }
            order.Total = total;
            await _orderRepository.AddAsync(order);
            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                CreatedAt = order.CreatedAt,
                Total = order.Total,
                Products = order.OrderProducts.Select(op => new OrderProductDto
                {
                    ProductId = op.ProductId,
                    ProductName = "",
                    Quantity = op.Quantity,
                    UnitPrice = op.UnitPrice
                }).ToList()
            };
        }
    }
} 