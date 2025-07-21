using Ecommerce.Domain.Entities;
using Ecommerce.Domain.InterfacesRepository;
using Moq;
using Xunit;

namespace Ecommerce.Tests.Domain.InterfacesRepository
{
    public class IProductRepositoryTests
    {
        [Fact]
        public async Task Deve_Chamar_Metodos_Basicos_Do_Contrato()
        {
            var mock = new Mock<IProductRepository>();
            var product = new Product { Id = Guid.NewGuid(), Name = "Produto" };
            await mock.Object.AddAsync(product);
            await mock.Object.UpdateAsync(product);
            await mock.Object.DeleteAsync(product.Id);
            await mock.Object.GetByIdAsync(product.Id);
            await mock.Object.GetAllAsync();
            mock.Verify(x => x.AddAsync(product), Times.Once);
            mock.Verify(x => x.UpdateAsync(product), Times.Once);
            mock.Verify(x => x.DeleteAsync(product.Id), Times.Once);
            mock.Verify(x => x.GetByIdAsync(product.Id), Times.Once);
            mock.Verify(x => x.GetAllAsync(), Times.Once);
        }
    }
} 