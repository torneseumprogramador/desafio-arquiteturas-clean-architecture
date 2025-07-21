using Ecommerce.Domain.InterfacesRepository;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EcommerceDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderProductRepository, OrderProductRepository>();

            return services;
        }
    }
} 