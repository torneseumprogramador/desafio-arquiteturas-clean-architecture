using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.UseCases.Users;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly CreateUserUseCase _createUserUseCase;
        private readonly IMapper _mapper;

        public UsersController(CreateUserUseCase createUserUseCase, IMapper mapper)
        {
            _createUserUseCase = createUserUseCase;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto dto)
        {
            var user = await _createUserUseCase.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        // Endpoint de exemplo para GetById (pode ser expandido)
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById([FromServices] Ecommerce.Domain.InterfacesRepository.IUserRepository repo, Guid id)
        {
            var user = await repo.GetByIdAsync(id);
            if (user == null) return NotFound();
            return _mapper.Map<UserDto>(user);
        }
    }
} 