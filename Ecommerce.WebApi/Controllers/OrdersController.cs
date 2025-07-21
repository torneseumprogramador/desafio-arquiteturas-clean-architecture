using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.UseCases.Orders;
using Ecommerce.Domain.InterfacesRepository;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly CreateOrderUseCase _createOrderUseCase;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrdersController(CreateOrderUseCase createOrderUseCase, IOrderRepository orderRepository, IMapper mapper)
        {
            _createOrderUseCase = createOrderUseCase;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderDto dto)
        {
            var order = await _createOrderUseCase.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            var orders = await _orderRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return NotFound();
            return _mapper.Map<OrderDto>(order);
        }
    }
} 