using AutoMapper;
using Ecommerce.Domain.Entities;
using Ecommerce.Application.DTOs;

namespace Ecommerce.WebApi.Profiles
{
    public class DomainToDtoProfile : Profile
    {
        public DomainToDtoProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.OrderProducts));
            CreateMap<OrderProduct, OrderProductDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty));

            // ReverseMap para criação
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            CreateMap<CreateProductDto, Product>();
            CreateMap<CreateOrderDto, Order>();
            CreateMap<CreateOrderProductDto, OrderProduct>();
        }
    }
} 