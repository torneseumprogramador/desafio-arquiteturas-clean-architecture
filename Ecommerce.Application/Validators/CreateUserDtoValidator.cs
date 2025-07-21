using Ecommerce.Application.DTOs;
using FluentValidation;

namespace Ecommerce.Application.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatório");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email inválido");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Senha deve ter pelo menos 6 caracteres");
        }
    }
} 