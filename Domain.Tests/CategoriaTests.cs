using System.ComponentModel.DataAnnotations;
using Xunit;
using Domain.Entidades;

namespace Domain.Tests
{
    public class CategoriaTests
    {
        [Fact]
        public void Categoria_ShouldInitializeProdutosCollection()
        {
            // Arrange & Act
            var categoria = new Categoria();

            // Assert
            Xunit.Assert.NotNull(categoria.Produtos);
            Xunit.Assert.Empty(categoria.Produtos);
        }

        [Fact]
        public void Categoria_Properties_ShouldHaveCorrectAttributes()
        {
            // Arrange
            var categoria = new Categoria();

            // Act
            var idProperty = typeof(Categoria).GetProperty(nameof(Categoria.Id));
            var nomeProperty = typeof(Categoria).GetProperty(nameof(Categoria.Nome));
            var descricaoProperty = typeof(Categoria).GetProperty(nameof(Categoria.Descricao));
            var ordemProperty = typeof(Categoria).GetProperty(nameof(Categoria.Ordem));

            // Assert
            Xunit.Assert.NotNull(idProperty);
            Xunit.Assert.NotNull(nomeProperty);
            Xunit.Assert.NotNull(descricaoProperty);
            Xunit.Assert.NotNull(ordemProperty);

            // Check attributes for Id
            var idAttributes = idProperty.GetCustomAttributes(typeof(KeyAttribute), false);
            Xunit.Assert.Contains(idAttributes, attr => attr is KeyAttribute);

            var requiredIdAttributes = idProperty.GetCustomAttributes(typeof(RequiredAttribute), false);
            Xunit.Assert.Contains(requiredIdAttributes, attr => attr is RequiredAttribute);

            // Check attributes for Nome
            var nomeAttributes = nomeProperty.GetCustomAttributes(typeof(RequiredAttribute), false);
            Xunit.Assert.Contains(nomeAttributes, attr => attr is RequiredAttribute);

            var maxLengthNomeAttributes = nomeProperty.GetCustomAttributes(typeof(MaxLengthAttribute), false);
            Xunit.Assert.Contains(maxLengthNomeAttributes, attr => ((MaxLengthAttribute)attr).Length == 30);

            // Check attributes for Descricao
            var maxLengthDescricaoAttributes = descricaoProperty.GetCustomAttributes(typeof(MaxLengthAttribute), false);
            Xunit.Assert.Contains(maxLengthDescricaoAttributes, attr => ((MaxLengthAttribute)attr).Length == 100);

            // Check attributes for Ordem
            var ordemAttributes = ordemProperty.GetCustomAttributes(typeof(RequiredAttribute), false);
            Xunit.Assert.Contains(ordemAttributes, attr => attr is RequiredAttribute);
        }
    }
}
