using Ecommerce.Application.DTOs;
using Ecommerce.Application.UseCases.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderProductsController : ControllerBase
    {
        private readonly AddProductToOrderUseCase _addProductToOrderUseCase;

        public OrderProductsController(AddProductToOrderUseCase addProductToOrderUseCase)
        {
            _addProductToOrderUseCase = addProductToOrderUseCase;
        }

        [HttpPost("{orderId}/add-product")]
        public async Task<IActionResult> AddProduct(Guid orderId, [FromBody] CreateOrderProductDto dto)
        {
            await _addProductToOrderUseCase.ExecuteAsync(orderId, dto);
            return NoContent();
        }
    }
} 