using Domain.Entidades;
using Lanchonete.infrastructure.Data;
using Lanchonete.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Infra.Tests
{
    public class PedidoItemRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly PedidoItemRepository _pedidoItemRepository;

        public PedidoItemRepositoryTests()
        {
            // Configure in-memory database
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(_options);
            _pedidoItemRepository = new PedidoItemRepository(_context);
        }

        [Fact]
        public void GetByPedidoId_ShouldReturnPedidoItems_WhenItemsExistForPedido()
        {
            // Arrange
            var pedido = new Pedido { Id = 100 };
            _context.Pedidos.Add(pedido);

            var pedidoItem1 = new PedidoItem { PedidoId = 100, ProdutoId = 1, Quantidade = 2, Valor = 20.0m };
            var pedidoItem2 = new PedidoItem { PedidoId = 100, ProdutoId = 2, Quantidade = 1, Valor = 15.0m };
            _context.PedidoItem.AddRange(pedidoItem1, pedidoItem2);
            _context.SaveChanges();

            // Act
            var pedidoItemsFromRepo = _pedidoItemRepository.GetByPedidoId(100);

            // Assert
            Xunit.Assert.NotNull(pedidoItemsFromRepo);
            Xunit.Assert.Equal(2, pedidoItemsFromRepo.Count);
            Xunit.Assert.Contains(pedidoItemsFromRepo, pi => pi.ProdutoId == 1 && pi.Quantidade == 2 && pi.Valor == 20.0m);
            Xunit.Assert.Contains(pedidoItemsFromRepo, pi => pi.ProdutoId == 2 && pi.Quantidade == 1 && pi.Valor == 15.0m);
        }

        [Fact]
        public void GetByPedidoId_ShouldReturnEmptyList_WhenNoItemsExistForPedido()
        {
            // Act
            var pedidoItemsFromRepo = _pedidoItemRepository.GetByPedidoId(999);

            // Assert
            Xunit.Assert.NotNull(pedidoItemsFromRepo);
            Xunit.Assert.Empty(pedidoItemsFromRepo);
        }
    }
}
