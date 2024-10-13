using Application.DTOs;
using Application.Repository;
using Application.UseCases;
using AutoMapper;
using Domain.Entidades;
using Domain.Enums;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class PedidoUseCaseTests
    {
        private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly Mock<IProdutoRepository> _produtoRepositoryMock;
        private readonly Mock<IPedidoItemRepository> _pedidoItemRepositoryMock;
        private readonly IMapper _mapper;
        private readonly PedidoUseCase _pedidoUseCase;

        public PedidoUseCaseTests()
        {
            _pedidoRepositoryMock = new Mock<IPedidoRepository>();
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _pedidoItemRepositoryMock = new Mock<IPedidoItemRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreatePedidoDTO, Pedido>();
                cfg.CreateMap<Pedido, ResponsePedidoDTO>();
                cfg.CreateMap<Cliente, ResponseClienteDTO>();
                cfg.CreateMap<ResponseClienteDTO, Cliente>();
                cfg.CreateMap<Produto, ResponseProdutoDTO>();
                cfg.CreateMap<ResponseProdutoDTO, Produto>();
            });
            _mapper = config.CreateMapper();

            _pedidoUseCase = new PedidoUseCase(_pedidoRepositoryMock.Object,
                                                _produtoRepositoryMock.Object,
                                                _pedidoItemRepositoryMock.Object,
                                                _clienteRepositoryMock.Object,                                                                                                
                                                _mapper);
        }

        [Fact]
        public void GetAll_ShouldReturnListOfPedidos_WhenPedidosExist()
        {
            // Arrange
            var pedidos = new List<Pedido>
            {
                new Pedido { Id = 1, ClienteId = 1, DataCriacao = DateTime.Now, Status = PedidoStatus.Recebido, Valor = 100 }
            };

            _pedidoRepositoryMock.Setup(repo => repo.GetAll()).Returns(pedidos);
            _clienteRepositoryMock.Setup(repo => repo.GetById(1)).Returns(new Cliente { Id = 1 });
            _pedidoItemRepositoryMock.Setup(repo => repo.GetByPedidoId(1)).Returns(new List<PedidoItem>
            {
                new PedidoItem { Id = 1, ProdutoId = 1, Quantidade = 2, Valor = 100 }
            });
            _produtoRepositoryMock.Setup(repo => repo.GetById(1)).Returns(new Produto { Id = 1, Valor = 50 });

            // Act
            var result = _pedidoUseCase.GetAll();

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Single(result);
            Xunit.Assert.Equal(1, result.First().Id);
        }
    }
}