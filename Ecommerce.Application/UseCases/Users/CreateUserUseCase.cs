using System.Threading.Tasks;
using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.InterfacesRepository;

namespace Ecommerce.Application.UseCases.Users
{
    public class CreateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        public CreateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> ExecuteAsync(CreateUserDto dto)
        {
            // Aqui seria feita a validação e hash da senha
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.Password, // Trocar por hash real em produção
                CreatedAt = DateTime.UtcNow
            };
            await _userRepository.AddAsync(user);
            return new UserDto { Id = user.Id, Name = user.Name, Email = user.Email };
        }
    }
} 