using System.ComponentModel.DataAnnotations;
using Xunit;
using Dominio.Entidades;

namespace Domain.Tests
{
    public class ProdutoTests
    {
        [Fact]
        public void Produto_ShouldInitializeWithConstructor()
        {
            // Arrange
            var nome = "Produto Teste";
            var descricao = "Descrição do Produto";
            var valor = 100.0m;
            var estoque = 10;
            var disponivel = true;
            var categoriaId = 1;
            var ordemExibicao = 1;

            // Act
            var produto = new Produto(nome, descricao, valor, estoque, disponivel, categoriaId, ordemExibicao);

            // Assert
            Xunit.Assert.Equal(nome, produto.Nome);
            Xunit.Assert.Equal(descricao, produto.Descricao);
            Xunit.Assert.Equal(valor, produto.Valor);
            Xunit.Assert.Equal(estoque, produto.Estoque);
            Xunit.Assert.Equal(disponivel, produto.Disponivel);
            Xunit.Assert.Equal(categoriaId, produto.CategoriaId);
            Xunit.Assert.Equal(ordemExibicao, produto.OrdemExibicao);
        }

        [Fact]
        public void Produto_ShouldInitializeDataCadastro()
        {
            // Arrange & Act
            var produto = new Produto
            {
                DataCadastro = DateTime.Now
            };

            // Assert
            Xunit.Assert.True(produto.DataCadastro <= DateTime.Now);
        }

        [Fact]
        public void Produto_Properties_ShouldHaveCorrectAttributes()
        {
            // Arrange
            var idProperty = typeof(Produto).GetProperty(nameof(Produto.Id));
            var nomeProperty = typeof(Produto).GetProperty(nameof(Produto.Nome));
            var descricaoProperty = typeof(Produto).GetProperty(nameof(Produto.Descricao));
            var valorProperty = typeof(Produto).GetProperty(nameof(Produto.Valor));
            var estoqueProperty = typeof(Produto).GetProperty(nameof(Produto.Estoque));
            var disponivelProperty = typeof(Produto).GetProperty(nameof(Produto.Disponivel));
            var categoriaIdProperty = typeof(Produto).GetProperty(nameof(Produto.CategoriaId));
            var ordemExibicaoProperty = typeof(Produto).GetProperty(nameof(Produto.OrdemExibicao));
            var dataCadastroProperty = typeof(Produto).GetProperty(nameof(Produto.DataCadastro));

            // Act
            var attributes = new
            {
                Id = idProperty.GetCustomAttributes(true),
                Nome = nomeProperty.GetCustomAttributes(true),
                Descricao = descricaoProperty.GetCustomAttributes(true),
                Valor = valorProperty.GetCustomAttributes(true),
                Estoque = estoqueProperty.GetCustomAttributes(true),
                Disponivel = disponivelProperty.GetCustomAttributes(true),
                CategoriaId = categoriaIdProperty.GetCustomAttributes(true),
                OrdemExibicao = ordemExibicaoProperty.GetCustomAttributes(true),
                DataCadastro = dataCadastroProperty.GetCustomAttributes(true)
            };

            // Assert
            Xunit.Assert.Contains(attributes.Id, attr => attr is KeyAttribute);
            Xunit.Assert.Contains(attributes.Nome, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.Nome, attr => attr is MaxLengthAttribute maxLength && maxLength.Length == 30);
            Xunit.Assert.Contains(attributes.Descricao, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.Descricao, attr => attr is MaxLengthAttribute maxLength && maxLength.Length == 100);
            Xunit.Assert.Contains(attributes.Valor, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.Estoque, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.Disponivel, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.CategoriaId, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.OrdemExibicao, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.DataCadastro, attr => attr is RequiredAttribute);
        }
    }
}
