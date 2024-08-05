using System.ComponentModel.DataAnnotations;
using Xunit;
using Dominio.Entidades;
using Dominio.Enums;

namespace Domain.Tests
{
    public class PedidoTests
    {
        [Fact]
        public void Pedido_ShouldInitializeWithDefaultConstructor()
        {
            // Act
            var pedido = new Pedido();

            // Xunit.Assert
            Xunit.Assert.NotNull(pedido);
            Xunit.Assert.NotNull(pedido.Itens);
        }

        [Fact]
        public void Pedido_Properties_ShouldHaveCorrectAttributes()
        {
            // Arrange
            var idProperty = typeof(Pedido).GetProperty(nameof(Pedido.Id));
            var clienteIdProperty = typeof(Pedido).GetProperty(nameof(Pedido.ClienteId));
            var statusProperty = typeof(Pedido).GetProperty(nameof(Pedido.Status));
            var itensProperty = typeof(Pedido).GetProperty(nameof(Pedido.Itens));
            var dataCriacaoProperty = typeof(Pedido).GetProperty(nameof(Pedido.DataCriacao));
            var valorProperty = typeof(Pedido).GetProperty(nameof(Pedido.Valor));

            // Act
            var attributes = new
            {
                Id = idProperty.GetCustomAttributes(true),
                ClienteId = clienteIdProperty.GetCustomAttributes(true),
                Status = statusProperty.GetCustomAttributes(true),
                Itens = itensProperty.GetCustomAttributes(true),
                DataCriacao = dataCriacaoProperty.GetCustomAttributes(true),
                Valor = valorProperty.GetCustomAttributes(true)
            };

            // Xunit.Assert
            Xunit.Assert.Contains(attributes.Id, attr => attr is KeyAttribute);
            Xunit.Assert.Contains(attributes.ClienteId, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.Status, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.Itens, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.DataCriacao, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.Valor, attr => attr is RequiredAttribute);
        }

        [Fact]
        public void Pedido_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var clienteId = 123;
            var status = PedidoStauts.Recebido;
            var itens = new List<PedidoItem>
        {
            new PedidoItem
            {
                Id = 1,
                PedidoId = 1,
                ProdutoId = 1,
                NomeProduto = "Produto 1",
                Quantidade = 2,
                Customizacao = "Customizacao 1",
                Valor = 10.00m,
                DataCriacao = DateTime.Now
            }
        };
            var dataCriacao = DateTime.Now;
            var valor = 20.00m;

            // Act
            var pedido = new Pedido(clienteId, status, itens, dataCriacao, valor);

            // Xunit.Assert
            Xunit.Assert.Equal(clienteId, pedido.ClienteId);
            Xunit.Assert.Equal(status, pedido.Status);
            Xunit.Assert.Equal(itens, pedido.Itens);
            Xunit.Assert.Equal(dataCriacao, pedido.DataCriacao);
            Xunit.Assert.Equal(valor, pedido.Valor);
        }
    }
}
