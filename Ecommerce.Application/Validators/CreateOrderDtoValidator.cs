using Ecommerce.Application.DTOs;
using FluentValidation;

namespace Ecommerce.Application.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Usuário é obrigatório");
            RuleFor(x => x.Products).NotNull().NotEmpty().WithMessage("Pedido deve conter ao menos um produto");
            RuleForEach(x => x.Products).SetValidator(new CreateOrderProductDtoValidator());
        }
    }

    public class CreateOrderProductDtoValidator : AbstractValidator<CreateOrderProductDto>
    {
        public CreateOrderProductDtoValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Produto é obrigatório");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantidade deve ser maior que zero");
        }
    }
} 