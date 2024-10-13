using Domain.Entidades;
using Domain.Enums;
using Lanchonete.infrastructure.Data;
using Lanchonete.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Infra.Tests
{
    public class PedidoRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly PedidoRepository _pedidoRepository;

        public PedidoRepositoryTests()
        {
            // Configure in-memory database
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(_options);
            _pedidoRepository = new PedidoRepository(_context);
        }

        [Fact]
        public void Save_ShouldAddPedidoToDatabase()
        {
            // Arrange
            var pedido = new Pedido { ClienteId = 1, Status = PedidoStatus.Recebido, DataCriacao = DateTime.Now, Valor = 100.0m };

            // Act
            _pedidoRepository.Save(pedido);
            var pedidoFromDb = _context.Pedidos.FirstOrDefault(p => p.Id == pedido.Id);

            // Assert
            Xunit.Assert.NotNull(pedidoFromDb);
            Xunit.Assert.Equal(pedido.ClienteId, pedidoFromDb.ClienteId);
            Xunit.Assert.Equal(pedido.Status, pedidoFromDb.Status);
            Xunit.Assert.Equal(pedido.DataCriacao, pedidoFromDb.DataCriacao);
            Xunit.Assert.Equal(pedido.Valor, pedidoFromDb.Valor);
        }

        [Fact]
        public void GetAll_ShouldReturnAllPedidos()
        {
            // Arrange
            var pedido1 = new Pedido { ClienteId = 1, Status = PedidoStatus.Recebido, DataCriacao = DateTime.Now, Valor = 100.0m };
            var pedido2 = new Pedido { ClienteId = 2, Status = PedidoStatus.Recebido, DataCriacao = DateTime.Now, Valor = 200.0m };
            _context.Pedidos.AddRange(pedido1, pedido2);
            _context.SaveChanges();

            // Act
            var pedidosFromRepo = _pedidoRepository.GetAll();

            // Assert
            Xunit.Assert.NotNull(pedidosFromRepo);
            Xunit.Assert.Equal(2, pedidosFromRepo.Count);
            Xunit.Assert.Contains(pedidosFromRepo, p => p.ClienteId == 1);
            Xunit.Assert.Contains(pedidosFromRepo, p => p.ClienteId == 2);
        }
    }
}
