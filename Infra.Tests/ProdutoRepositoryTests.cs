using Dominio.Entidades;
using infra.Data;
using infra.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Infra.Tests
{
    public class ProdutoRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly ProdutoRepository _produtoRepository;

        public ProdutoRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(_options);
            _produtoRepository = new ProdutoRepository(_context);
        }

        [Fact]
        public void Save_ShouldAddProdutoToDatabase()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Produto Teste",
                Descricao = "Descrição Teste",
                Valor = 100,
                CategoriaId = 1,
                OrdemExibicao = 1,
                Estoque = 10,
                Disponivel = true,
                DataCadastro = DateTime.Now
            };

            // Act
            _produtoRepository.Save(produto);

            // Assert
            var savedProduto = _context.Produtos.FirstOrDefault(p => p.Id == produto.Id);
            Xunit.Assert.NotNull(savedProduto);
            Xunit.Assert.Equal(produto.Nome, savedProduto.Nome);
        }

        [Fact]
        public void Update_ShouldModifyProdutoInDatabase()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Produto Teste",
                Descricao = "Descrição Teste",
                Valor = 100,
                CategoriaId = 1,
                OrdemExibicao = 1,
                Estoque = 10,
                Disponivel = true,
                DataCadastro = DateTime.Now
            };

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            produto.Nome = "Produto Atualizado";

            // Act
            _produtoRepository.Update(produto);

            // Assert
            var updatedProduto = _context.Produtos.FirstOrDefault(p => p.Id == produto.Id);
            Xunit.Assert.NotNull(updatedProduto);
            Xunit.Assert.Equal("Produto Atualizado", updatedProduto.Nome);
        }

        [Fact]
        public void GetById_ShouldReturnProduto()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Produto Teste",
                Descricao = "Descrição Teste",
                Valor = 100,
                CategoriaId = 1,
                OrdemExibicao = 1,
                Estoque = 10,
                Disponivel = true,
                DataCadastro = DateTime.Now
            };

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            // Act
            var foundProduto = _produtoRepository.GetById(produto.Id);

            // Assert
            Xunit.Assert.NotNull(foundProduto);
            Xunit.Assert.Equal(produto.Nome, foundProduto.Nome);
        }

        [Fact]
        public void GetAll_ShouldReturnAllProdutos()
        {
            // Arrange
            var produto1 = new Produto
            {
                Nome = "Produto Teste 1",
                Descricao = "Descrição Teste 1",
                Valor = 100,
                CategoriaId = 1,
                OrdemExibicao = 1,
                Estoque = 10,
                Disponivel = true,
                DataCadastro = DateTime.Now
            };

            var produto2 = new Produto
            {
                Nome = "Produto Teste 2",
                Descricao = "Descrição Teste 2",
                Valor = 200,
                CategoriaId = 2,
                OrdemExibicao = 2,
                Estoque = 20,
                Disponivel = false,
                DataCadastro = DateTime.Now
            };

            _context.Produtos.AddRange(produto1, produto2);
            _context.SaveChanges();

            // Act
            var produtos = _produtoRepository.GetAll();

            // Assert
            Xunit.Assert.NotNull(produtos);
        }

        [Fact]
        public void GetByCategory_ShouldReturnProdutosByCategory()
        {
            // Arrange
            var produto1 = new Produto
            {
                Nome = "Produto Teste 1",
                Descricao = "Descrição Teste 1",
                Valor = 100,
                CategoriaId = 1,
                OrdemExibicao = 1,
                Estoque = 10,
                Disponivel = true,
                DataCadastro = DateTime.Now
            };

            var produto2 = new Produto
            {
                Nome = "Produto Teste 2",
                Descricao = "Descrição Teste 2",
                Valor = 200,
                CategoriaId = 2,
                OrdemExibicao = 2,
                Estoque = 20,
                Disponivel = false,
                DataCadastro = DateTime.Now
            };

            _context.Produtos.AddRange(produto1, produto2);
            _context.SaveChanges();

            // Act
            var produtos = _produtoRepository.GetByCategory(1);

            // Assert
            Xunit.Assert.NotNull(produtos);
            Xunit.Assert.Equal(1, produtos.First().CategoriaId);
        }

        [Fact]
        public void Delete_ShouldRemoveProdutoFromDatabase()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Produto Teste",
                Descricao = "Descrição Teste",
                Valor = 100,
                CategoriaId = 1,
                OrdemExibicao = 1,
                Estoque = 10,
                Disponivel = true,
                DataCadastro = DateTime.Now
            };

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            // Act
            _produtoRepository.Delete(produto.Id);

            // Assert
            var deletedProduto = _context.Produtos.FirstOrDefault(p => p.Id == produto.Id);
            Xunit.Assert.Null(deletedProduto);
        }
    }
}
