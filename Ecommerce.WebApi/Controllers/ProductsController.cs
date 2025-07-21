using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Domain.InterfacesRepository;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto dto)
        {
            var product = _mapper.Map<Ecommerce.Domain.Entities.Product>(dto);
            await _productRepository.AddAsync(product);
            var result = _mapper.Map<ProductDto>(product);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();
            return _mapper.Map<ProductDto>(product);
        }
    }
} 