using System;
using System.Collections.Generic;

namespace Ecommerce.Application.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Total { get; set; }
        public List<OrderProductDto> Products { get; set; }
    }

    public class CreateOrderDto
    {
        public Guid UserId { get; set; }
        public List<CreateOrderProductDto> Products { get; set; }
    }
} 