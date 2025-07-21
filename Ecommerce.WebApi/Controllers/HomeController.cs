using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApi.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// Página inicial da API
        /// </summary>
        [HttpGet]
        public ActionResult<object> Get()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            return Ok(new
            {
                title = "🛒 Ecommerce Clean Architecture API",
                description = "API de ecommerce desenvolvida com Clean Architecture, .NET 8, EF Core, AutoMapper, FluentValidation e Swagger.",
                version = "1.0.0",
                author = "Prof. Danilo Aparecido",
                course = "Arquiteturas de Software Modernas",
                platform = "Torne-se um Programador",
                links = new
                {
                    health = $"{baseUrl}/api/health",
                    swagger = $"{baseUrl}/swagger",
                    documentation = $"{baseUrl}/swagger/index.html"
                },
                endpoints = new
                {
                    users = $"{baseUrl}/api/users",
                    products = $"{baseUrl}/api/products",
                    orders = $"{baseUrl}/api/orders"
                },
                architecture = new
                {
                    pattern = "Clean Architecture",
                    layers = new[] { "WebApi", "Application", "Domain", "Infrastructure" },
                    database = "SQLite",
                    orm = "Entity Framework Core"
                },
                timestamp = DateTime.UtcNow
            });
        }
    }
} 