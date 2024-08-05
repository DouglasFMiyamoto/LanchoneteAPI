using System.ComponentModel.DataAnnotations;
using Xunit;
using Dominio.Entidades;

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
            var nomeProdutoProperty = typeof(PedidoItem).GetProperty(nameof(PedidoItem.NomeProduto));
            var quantidadeProperty = typeof(PedidoItem).GetProperty(nameof(PedidoItem.Quantidade));
            var customizacaoProperty = typeof(PedidoItem).GetProperty(nameof(PedidoItem.Customizacao));
            var valorProperty = typeof(PedidoItem).GetProperty(nameof(PedidoItem.Valor));
            var dataCriacaoProperty = typeof(PedidoItem).GetProperty(nameof(PedidoItem.DataCriacao));

            // Act
            var attributes = new
            {
                Id = idProperty.GetCustomAttributes(true),
                PedidoId = pedidoIdProperty.GetCustomAttributes(true),
                ProdutoId = produtoIdProperty.GetCustomAttributes(true),
                NomeProduto = nomeProdutoProperty.GetCustomAttributes(true),
                Quantidade = quantidadeProperty.GetCustomAttributes(true),
                Customizacao = customizacaoProperty.GetCustomAttributes(true),
                Valor = valorProperty.GetCustomAttributes(true),
                DataCriacao = dataCriacaoProperty.GetCustomAttributes(true)
            };

            // Xunit.Assert
            Xunit.Assert.Contains(attributes.Id, attr => attr is KeyAttribute);
            Xunit.Assert.Contains(attributes.PedidoId, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.ProdutoId, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.NomeProduto, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.NomeProduto, attr => attr is MaxLengthAttribute maxLength && maxLength.Length == 30);
            Xunit.Assert.Contains(attributes.Quantidade, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.Customizacao, attr => attr is MaxLengthAttribute maxLength && maxLength.Length == 200);
            Xunit.Assert.Contains(attributes.Valor, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.DataCriacao, attr => attr is RequiredAttribute);
        }

        [Fact]
        public void PedidoItem_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var id = 1;
            var pedidoId = 123;
            var produtoId = 456;
            var nomeProduto = "Produto Teste";
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
                NomeProduto = nomeProduto,
                Quantidade = quantidade,
                Customizacao = customizacao,
                Valor = valor,
                DataCriacao = dataCriacao
            };

            // Xunit.Assert
            Xunit.Assert.Equal(id, pedidoItem.Id);
            Xunit.Assert.Equal(pedidoId, pedidoItem.PedidoId);
            Xunit.Assert.Equal(produtoId, pedidoItem.ProdutoId);
            Xunit.Assert.Equal(nomeProduto, pedidoItem.NomeProduto);
            Xunit.Assert.Equal(quantidade, pedidoItem.Quantidade);
            Xunit.Assert.Equal(customizacao, pedidoItem.Customizacao);
            Xunit.Assert.Equal(valor, pedidoItem.Valor);
            Xunit.Assert.Equal(dataCriacao, pedidoItem.DataCriacao);
        }
    }
}
