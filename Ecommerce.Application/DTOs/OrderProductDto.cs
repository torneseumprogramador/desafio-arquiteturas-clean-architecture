namespace Ecommerce.Application.DTOs
{
    public class OrderProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class CreateOrderProductDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
} 