using Ecommerce.Domain.Entities;
using Ecommerce.Domain.InterfacesRepository;
using Moq;
using Xunit;

namespace Ecommerce.Tests.Domain.InterfacesRepository
{
    public class IOrderProductRepositoryTests
    {
        [Fact]
        public async Task Deve_Chamar_Metodos_Basicos_Do_Contrato()
        {
            var mock = new Mock<IOrderProductRepository>();
            var op = new OrderProduct { OrderId = Guid.NewGuid(), ProductId = Guid.NewGuid() };
            await mock.Object.AddAsync(op);
            await mock.Object.UpdateAsync(op);
            await mock.Object.DeleteAsync(op.OrderId, op.ProductId);
            await mock.Object.GetByIdAsync(op.OrderId, op.ProductId);
            await mock.Object.GetByOrderIdAsync(op.OrderId);
            mock.Verify(x => x.AddAsync(op), Times.Once);
            mock.Verify(x => x.UpdateAsync(op), Times.Once);
            mock.Verify(x => x.DeleteAsync(op.OrderId, op.ProductId), Times.Once);
            mock.Verify(x => x.GetByIdAsync(op.OrderId, op.ProductId), Times.Once);
            mock.Verify(x => x.GetByOrderIdAsync(op.OrderId), Times.Once);
        }
    }
} 