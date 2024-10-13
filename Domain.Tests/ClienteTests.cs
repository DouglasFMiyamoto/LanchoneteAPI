using System.ComponentModel.DataAnnotations;
using Xunit;
using Domain.Entidades;

namespace Domain.Tests
{
    public class ClienteTests
    {
        [Fact]
        public void Cliente_ShouldInitializeWithDefaultConstructor()
        {
            // Act
            var cliente = new Cliente();

            // Xunit.Assert
            Xunit.Assert.NotNull(cliente);
            Xunit.Assert.Equal(default, cliente.Id);
            Xunit.Assert.Equal(string.Empty, cliente.Nome);
            Xunit.Assert.Equal(string.Empty, cliente.Cpf);
            Xunit.Assert.Equal(string.Empty, cliente.Email);
            Xunit.Assert.Equal(string.Empty, cliente.Telefone);
            Xunit.Assert.Equal(default, cliente.DataNascimento);
            Xunit.Assert.Equal(default, cliente.DataCriacao);
        }

        [Fact]
        public void Cliente_Properties_ShouldHaveCorrectAttributes()
        {
            // Arrange
            var idProperty = typeof(Cliente).GetProperty(nameof(Cliente.Id));
            var nomeProperty = typeof(Cliente).GetProperty(nameof(Cliente.Nome));
            var cpfProperty = typeof(Cliente).GetProperty(nameof(Cliente.Cpf));
            var emailProperty = typeof(Cliente).GetProperty(nameof(Cliente.Email));
            var telefoneProperty = typeof(Cliente).GetProperty(nameof(Cliente.Telefone));
            var dataNascimentoProperty = typeof(Cliente).GetProperty(nameof(Cliente.DataNascimento));
            var dataCriacaoProperty = typeof(Cliente).GetProperty(nameof(Cliente.DataCriacao));

            // Act
            var attributes = new
            {
                Id = idProperty.GetCustomAttributes(true),
                Nome = nomeProperty.GetCustomAttributes(true),
                Cpf = cpfProperty.GetCustomAttributes(true),
                Email = emailProperty.GetCustomAttributes(true),
                Telefone = telefoneProperty.GetCustomAttributes(true),
                DataNascimento = dataNascimentoProperty.GetCustomAttributes(true),
                DataCriacao = dataCriacaoProperty.GetCustomAttributes(true)
            };

            // Xunit.Assert
            Xunit.Assert.Contains(attributes.Id, attr => attr is KeyAttribute);
            Xunit.Assert.Contains(attributes.Id, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.Nome, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.Nome, attr => attr is MaxLengthAttribute maxLength && maxLength.Length == 100);
            Xunit.Assert.Contains(attributes.Cpf, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.Cpf, attr => attr is MaxLengthAttribute maxLength && maxLength.Length == 11);
            Xunit.Assert.Contains(attributes.Email, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.Email, attr => attr is MaxLengthAttribute maxLength && maxLength.Length == 100);
            Xunit.Assert.Contains(attributes.Telefone, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.Telefone, attr => attr is MaxLengthAttribute maxLength && maxLength.Length == 11);
            Xunit.Assert.Contains(attributes.DataNascimento, attr => attr is RequiredAttribute);
            Xunit.Assert.Contains(attributes.DataCriacao, attr => attr is RequiredAttribute);
        }

        [Fact]
        public void Cliente_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var id = 1;
            var nome = "João da Silva";
            var cpf = "12345678901";
            var email = "joao.silva@example.com";
            var telefone = "11987654321";
            var dataNascimento = new DateTime(1990, 1, 1);
            var dataCriacao = DateTime.Now;

            // Act
            var cliente = new Cliente
            {
                Id = id,
                Nome = nome,
                Cpf = cpf,
                Email = email,
                Telefone = telefone,
                DataNascimento = dataNascimento,
                DataCriacao = dataCriacao
            };

            // Xunit.Assert
            Xunit.Assert.Equal(id, cliente.Id);
            Xunit.Assert.Equal(nome, cliente.Nome);
            Xunit.Assert.Equal(cpf, cliente.Cpf);
            Xunit.Assert.Equal(email, cliente.Email);
            Xunit.Assert.Equal(telefone, cliente.Telefone);
            Xunit.Assert.Equal(dataNascimento, cliente.DataNascimento);
            Xunit.Assert.Equal(dataCriacao, cliente.DataCriacao);
        }
    }
}
