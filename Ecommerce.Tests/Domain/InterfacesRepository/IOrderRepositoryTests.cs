using Ecommerce.Domain.Entities;
using Ecommerce.Domain.InterfacesRepository;
using Moq;
using Xunit;

namespace Ecommerce.Tests.Domain.InterfacesRepository
{
    public class IOrderRepositoryTests
    {
        [Fact]
        public async Task Deve_Chamar_Metodos_Basicos_Do_Contrato()
        {
            var mock = new Mock<IOrderRepository>();
            var order = new Order { Id = Guid.NewGuid() };
            await mock.Object.AddAsync(order);
            await mock.Object.UpdateAsync(order);
            await mock.Object.DeleteAsync(order.Id);
            await mock.Object.GetByIdAsync(order.Id);
            await mock.Object.GetAllAsync();
            mock.Verify(x => x.AddAsync(order), Times.Once);
            mock.Verify(x => x.UpdateAsync(order), Times.Once);
            mock.Verify(x => x.DeleteAsync(order.Id), Times.Once);
            mock.Verify(x => x.GetByIdAsync(order.Id), Times.Once);
            mock.Verify(x => x.GetAllAsync(), Times.Once);
        }
    }
} 