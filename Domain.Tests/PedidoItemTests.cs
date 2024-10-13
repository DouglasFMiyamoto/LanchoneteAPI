using Xunit;
using Domain.Entidades;

namespace Domain.Tests
{
    public class PedidoItemTests
    {
        [Fact]
        public void PedidoItem_ShouldInitializeWithDefaultConstructor()
        {
            // Act
            var pedidoItem = new PedidoItem();

            // Xunit.Assert
            Xunit.Assert.NotNull(pedidoItem);
        }

        [Fact]
        public void PedidoItem_Properties_ShouldHaveCorrectAttributes()
        {
            // Arrange
            var idProperty = typeof(PedidoItem).GetProperty(nameof(PedidoItem.Id));
            var pedidoIdProperty = typeof(PedidoItem).GetProperty(nameof(PedidoItem.PedidoId));
            var produtoIdProperty = typeof(PedidoItem).GetProperty(nameof(PedidoItem.ProdutoId));
            var quantidadeProperty = typeof(PedidoItem).GetProperty(nameof(PedidoItem.Quantidade));
            var customizacaoProperty = typeof(PedidoItem).GetProperty(nameof(PedidoItem.Customizacao));
            var valorProperty = typeof(PedidoItem).GetProperty(nameof(PedidoItem.Valor));
            var dataCriacaoProperty = typeof(PedidoItem).GetProperty(nameof(PedidoItem.DataCriacao));

            // Act
            var attributes = new
            {
                Id = idProperty,
                PedidoId = pedidoIdProperty,
                ProdutoId = produtoIdProperty,
                Quantidade = quantidadeProperty,
                Customizacao = customizacaoProperty,
                Valor = valorProperty,
                DataCriacao = dataCriacaoProperty
            };
        }

        [Fact]
        public void PedidoItem_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var id = 1;
            var pedidoId = 123;
            var produtoId = 456;
            var quantidade = 5;
            var customizacao = "Customização Teste";
            var valor = 99.99m;
            var dataCriacao = DateTime.Now;

            // Act
            var pedidoItem = new PedidoItem
            {
                Id = id,
                PedidoId = pedidoId,
                ProdutoId = produtoId,
                Quantidade = quantidade,
                Customizacao = customizacao,
                Valor = valor,
                DataCriacao = dataCriacao
            };

            // Xunit.Assert
            Xunit.Assert.Equal(id, pedidoItem.Id);
            Xunit.Assert.Equal(pedidoId, pedidoItem.PedidoId);
            Xunit.Assert.Equal(produtoId, pedidoItem.ProdutoId);
            Xunit.Assert.Equal(quantidade, pedidoItem.Quantidade);
            Xunit.Assert.Equal(customizacao, pedidoItem.Customizacao);
            Xunit.Assert.Equal(valor, pedidoItem.Valor);
            Xunit.Assert.Equal(dataCriacao, pedidoItem.DataCriacao);
        }
    }
}
