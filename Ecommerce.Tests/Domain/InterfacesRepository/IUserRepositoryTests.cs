using Ecommerce.Domain.Entities;
using Ecommerce.Domain.InterfacesRepository;
using Moq;
using Xunit;

namespace Ecommerce.Tests.Domain.InterfacesRepository
{
    public class IUserRepositoryTests
    {
        [Fact]
        public async Task Deve_Chamar_Metodos_Basicos_Do_Contrato()
        {
            var mock = new Mock<IUserRepository>();
            var user = new User { Id = Guid.NewGuid(), Name = "Teste", Email = "teste@email.com" };
            await mock.Object.AddAsync(user);
            await mock.Object.UpdateAsync(user);
            await mock.Object.DeleteAsync(user.Id);
            await mock.Object.GetByIdAsync(user.Id);
            await mock.Object.GetByEmailAsync(user.Email);
            await mock.Object.GetAllAsync();
            mock.Verify(x => x.AddAsync(user), Times.Once);
            mock.Verify(x => x.UpdateAsync(user), Times.Once);
            mock.Verify(x => x.DeleteAsync(user.Id), Times.Once);
            mock.Verify(x => x.GetByIdAsync(user.Id), Times.Once);
            mock.Verify(x => x.GetByEmailAsync(user.Email), Times.Once);
            mock.Verify(x => x.GetAllAsync(), Times.Once);
        }
    }
} 