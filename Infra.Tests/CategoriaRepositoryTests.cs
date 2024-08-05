using Dominio.Entidades;
using infra.Data;
using infra.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Infra.Tests
{
    public class CategoriaRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly CategoriaRepository _categoriaRepository;

        public CategoriaRepositoryTests()
        {
            // Configure in-memory database
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(_options);
            _categoriaRepository = new CategoriaRepository(_context);
        }

        [Fact]
        public void GetAll_ShouldReturnAllCategorias()
        {
            // Arrange
            var categoria1 = new Categoria { Nome = "Categoria Teste 1" };
            var categoria2 = new Categoria { Nome = "Categoria Teste 2" };

            _context.Categorias.AddRange(categoria1, categoria2);
            _context.SaveChanges();

            // Act
            var categorias = _categoriaRepository.GetAll();

            // Assert
            Xunit.Assert.NotNull(categorias);
            Xunit.Assert.Equal(2, categorias.Count);
            Xunit.Assert.Contains(categorias, c => c.Nome == "Categoria Teste 1");
            Xunit.Assert.Contains(categorias, c => c.Nome == "Categoria Teste 2");
        }

        [Fact]
        public void GetAll_ShouldReturnEmptyList_WhenNoCategoriasExist()
        {
            // Arrange
            // Ensure the database is empty
            _context.Categorias.RemoveRange(_context.Categorias);
            _context.SaveChanges();

            // Act
            var categorias = _categoriaRepository.GetAll();

            // Assert
            Xunit.Assert.NotNull(categorias);
            Xunit.Assert.Empty(categorias);
        }
    }
}
