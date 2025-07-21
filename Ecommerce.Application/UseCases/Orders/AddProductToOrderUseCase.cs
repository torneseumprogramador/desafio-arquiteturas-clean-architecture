using System;
using System.Threading.Tasks;
using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.InterfacesRepository;

namespace Ecommerce.Application.UseCases.Orders
{
    public class AddProductToOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderProductRepository _orderProductRepository;

        public AddProductToOrderUseCase(IOrderRepository orderRepository, IProductRepository productRepository, IOrderProductRepository orderProductRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderProductRepository = orderProductRepository;
        }

        public async Task ExecuteAsync(Guid orderId, CreateOrderProductDto dto)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null) throw new Exception("Pedido não encontrado");
            var product = await _productRepository.GetByIdAsync(dto.ProductId);
            if (product == null) throw new Exception("Produto não encontrado");
            if (product.Stock < dto.Quantity) throw new Exception("Estoque insuficiente");
            var orderProduct = new OrderProduct
            {
                OrderId = orderId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                UnitPrice = product.Price
            };
            await _orderProductRepository.AddAsync(orderProduct);
        }
    }
} 