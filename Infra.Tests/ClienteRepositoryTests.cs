using Domain.Entidades;
using Lanchonete.infrastructure.Data;
using Lanchonete.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Infra.Tests
{
    public class ClienteRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly ClienteRepository _clienteRepository;

        public ClienteRepositoryTests()
        {
            // Configure in-memory database
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(_options);
            _clienteRepository = new ClienteRepository(_context);
        }

        [Fact]
        public void Save_ShouldAddClienteToDatabase()
        {
            // Arrange
            var cliente = new Cliente { Cpf = "12345678901", Nome = "Teste Cliente" };

            // Act
            _clienteRepository.Save(cliente);
            var clienteFromDb = _context.Clientes.FirstOrDefault(c => c.Cpf == "12345678901");

            // Assert
            Xunit.Assert.NotNull(clienteFromDb);
            Xunit.Assert.Equal("12345678901", clienteFromDb.Cpf);
            Xunit.Assert.Equal("Teste Cliente", clienteFromDb.Nome);
        }

        [Fact]
        public void GetById_ShouldReturnCliente_WhenClienteExists()
        {
            // Arrange
            var cliente = new Cliente { Cpf = "12345678901", Nome = "Teste Cliente" };
            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            // Act
            var clienteFromRepo = _clienteRepository.GetById(cliente.Id);

            // Assert
            Xunit.Assert.NotNull(clienteFromRepo);
            Xunit.Assert.Equal(cliente.Id, clienteFromRepo.Id);
            Xunit.Assert.Equal("12345678901", clienteFromRepo.Cpf);
            Xunit.Assert.Equal("Teste Cliente", clienteFromRepo.Nome);
        }

        [Fact]
        public void GetById_ShouldReturnNull_WhenClienteDoesNotExist()
        {
            // Act
            var clienteFromRepo = _clienteRepository.GetById(999);

            // Assert
            Xunit.Assert.Null(clienteFromRepo);
        }

        [Fact]
        public void GetByDocument_ShouldReturnCliente_WhenClienteExists()
        {
            // Arrange
            var cliente = new Cliente { Cpf = "32345678901", Nome = "Teste Cliente" };
            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            // Act
            var clienteFromRepo = _clienteRepository.GetByDocument("32345678901");

            // Assert
            Xunit.Assert.NotNull(clienteFromRepo);
            Xunit.Assert.Equal(cliente.Id, clienteFromRepo.Id);
            Xunit.Assert.Equal("32345678901", clienteFromRepo.Cpf);
            Xunit.Assert.Equal("Teste Cliente", clienteFromRepo.Nome);
        }

        [Fact]
        public void GetByDocument_ShouldReturnNull_WhenClienteDoesNotExist()
        {
            // Act
            var clienteFromRepo = _clienteRepository.GetByDocument("99999999999");

            // Assert
            Xunit.Assert.Null(clienteFromRepo);
        }
    }
}
