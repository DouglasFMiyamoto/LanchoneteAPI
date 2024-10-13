using Application.DTOs;
using Application.Repository;
using Application.UseCases;
using AutoMapper;
using Domain.Entidades;
using Moq;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Application.Tests
{
    public class ProdutoUseCaseTests
    {
        private readonly Mock<IProdutoRepository> _produtoRepositoryMock;
        private readonly Mock<ICategoriaRepository> _categoriaRepositoryMock;
        private readonly IMapper _mapper;
        private readonly ProdutoUseCase _produtoUseCase;

        public ProdutoUseCaseTests()
        {
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _categoriaRepositoryMock = new Mock<ICategoriaRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateProdutoDTO, Produto>();
                cfg.CreateMap<UpdateProdutoDTO, Produto>();
                cfg.CreateMap<Produto, ResponseProdutoDTO>();
            });
            _mapper = config.CreateMapper();

            _produtoUseCase = new ProdutoUseCase(_produtoRepositoryMock.Object, _mapper, _categoriaRepositoryMock.Object);
        }

        [Fact]
        public void Save_ShouldCallRepositorySave_WhenValidProduto()
        {
            // Arrange
            var produtoDTO = new CreateProdutoDTO { Nome = "Produto Teste", Valor = 10, CategoriaId = 1, OrdemExibicao = 1 };
            var produto = _mapper.Map<Produto>(produtoDTO);
            _categoriaRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<Categoria> { new Categoria { Id = 1, Nome = "Categoria Teste" } });

            // Act
            _produtoUseCase.Save(produtoDTO);

            // Assert
            _produtoRepositoryMock.Verify(repo => repo.Save(It.Is<Produto>(p => p.Nome == produto.Nome)), Times.Once);
        }

        [Fact]
        public void Save_ShouldThrowValidationException_WhenInvalidProduto()
        {
            // Arrange
            var produtoDTO = new CreateProdutoDTO { Nome = "Produto Teste", Valor = 0, CategoriaId = 1, OrdemExibicao = 1 };
            _categoriaRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<Categoria> { new Categoria { Id = 1, Nome = "Categoria Teste" } });

            // Act & Assert
            Xunit.Assert.Throws<ValidationException>(() => _produtoUseCase.Save(produtoDTO));
        }

        [Fact]
        public void Update_ShouldCallRepositoryUpdate_WhenValidProduto()
        {
            // Arrange
            var produtoDTO = new UpdateProdutoDTO { Nome = "Produto Teste", Valor = 10, CategoriaId = 1, OrdemExibicao = 1, Descricao = "Sem alface", Disponivel = true, Estoque = 10 };
            var produto = new Produto { Id = 1, Nome = "Produto Original", Valor = 5, CategoriaId = 1, OrdemExibicao = 2, Descricao = "Sem molho", Disponivel = true, Estoque = 15 };
            _produtoRepositoryMock.Setup(repo => repo.GetById(1)).Returns(produto);
            _categoriaRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<Categoria> { new Categoria { Id = 1, Nome = "Categoria Teste" } });

            // Act
            _produtoUseCase.Update(produtoDTO, 1);

            // Assert
            _produtoRepositoryMock.Verify(repo => repo.Update(It.Is<Produto>(p => p.Nome == produtoDTO.Nome && p.Valor == produtoDTO.Valor)), Times.Once);
        }

        [Fact]
        public void GetById_ShouldReturnProdutoDTO_WhenProdutoExists()
        {
            // Arrange
            var produto = new Produto { Id = 1, Nome = "Produto Teste", Valor = 10, CategoriaId = 1, OrdemExibicao = 1 };
            _produtoRepositoryMock.Setup(repo => repo.GetById(1)).Returns(produto);

            // Act
            var result = _produtoUseCase.GetById(1);

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(produto.Nome, result.Nome);
        }

        [Fact]
        public void Delete_ShouldCallRepositoryDelete_WhenCalled()
        {
            // Act
            _produtoUseCase.Delete(1);

            // Assert
            _produtoRepositoryMock.Verify(repo => repo.Delete(1), Times.Once);
        }
    }
}