using Application.DTOs;
using Application.Repository;
using Application.UseCases;
using Application.Utils;
using AutoMapper;
using Dominio.Entidades;
using Moq;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Application.Tests
{
    public class ClienteUseCaseTests
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly IMapper _mapper;
        private readonly ClienteUseCase _clienteUseCase;

        public ClienteUseCaseTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateClienteDTO, Cliente>();
                cfg.CreateMap<Cliente, ResponseClienteDTO>();
            });
            _mapper = config.CreateMapper();

            _clienteUseCase = new ClienteUseCase(_clienteRepositoryMock.Object, _mapper);
        }

        [Fact]
        public void Save_ShouldCallRepositorySave_WhenValidCliente()
        {
            // Arrange
            var clienteDTO = new CreateClienteDTO
            {
                Nome = "Cliente Teste",
                Cpf = "106.010.500-45",
                Email = "cliente@teste.com",
                Telefone = "11999999999",
                DataNascimento = new DateTime(2000, 1, 1)
            };

            _clienteRepositoryMock.Setup(repo => repo.GetByDocument(It.IsAny<string>())).Returns((Cliente) null);
            CpfUtils.IsValidCpf(clienteDTO.Cpf).Equals(true);
            EmailUtils.IsValidEmail(clienteDTO.Email).Equals(true);
            PhoneUtils.IsValidPhoneNumber(clienteDTO.Telefone).Equals(true);

            // Act
            _clienteUseCase.Save(clienteDTO);

            // Assert
            _clienteRepositoryMock.Verify(repo => repo.Save(It.IsAny<Cliente>()), Times.Once);
        }

        [Fact]
        public void Save_ShouldThrowValidationException_WhenClienteCpfAlreadyExists()
        {
            // Arrange
            var clienteDTO = new CreateClienteDTO
            {
                Nome = "Cliente Teste",
                Cpf = "12345678909",
                Email = "cliente@teste.com",
                Telefone = "11999999999",
                DataNascimento = new DateTime(2000, 1, 1)
            };

            _clienteRepositoryMock.Setup(repo => repo.GetByDocument(It.IsAny<string>())).Returns(new Cliente { Cpf = clienteDTO.Cpf });

            // Act & Assert
            Xunit.Assert.Throws<ValidationException>(() => _clienteUseCase.Save(clienteDTO));
        }

        [Fact]
        public void Save_ShouldThrowValidationException_WhenCpfIsInvalid()
        {
            // Arrange
            var clienteDTO = new CreateClienteDTO
            {
                Nome = "Cliente Teste",
                Cpf = "12345678909",
                Email = "cliente@teste.com",
                Telefone = "11999999999",
                DataNascimento = new DateTime(2000, 1, 1)
            };

            _clienteRepositoryMock.Setup(repo => repo.GetByDocument(It.IsAny<string>())).Returns(new Cliente());
            CpfUtils.IsValidCpf(clienteDTO.Cpf).Equals(false);

            // Act & Assert
            Xunit.Assert.Throws<ValidationException>(() => _clienteUseCase.Save(clienteDTO));
        }

        [Fact]
        public void Save_ShouldThrowValidationException_WhenEmailIsInvalid()
        {
            // Arrange
            var clienteDTO = new CreateClienteDTO
            {
                Nome = "Cliente Teste",
                Cpf = "10987654321",
                Email = "cliente@teste.com",
                Telefone = "11999999999",
                DataNascimento = new DateTime(2000, 1, 1)
            };

            _clienteRepositoryMock.Setup(repo => repo.GetByDocument(It.IsAny<string>())).Returns(new Cliente());
            CpfUtils.IsValidCpf(clienteDTO.Cpf).Equals(true);
            EmailUtils.IsValidEmail(clienteDTO.Email).Equals(false);

            // Act & Assert
            Xunit.Assert.Throws<ValidationException>(() => _clienteUseCase.Save(clienteDTO));
        }

        [Fact]
        public void Save_ShouldThrowValidationException_WhenPhoneNumberIsInvalid()
        {
            // Arrange
            var clienteDTO = new CreateClienteDTO
            {
                Nome = "Cliente Teste",
                Cpf = "12345678909",
                Email = "cliente@teste.com",
                Telefone = "11999999999",
                DataNascimento = new DateTime(2000, 1, 1)
            };

            _clienteRepositoryMock.Setup(repo => repo.GetByDocument(It.IsAny<string>())).Returns(new Cliente());
            CpfUtils.IsValidCpf(clienteDTO.Cpf).Equals(true);
            EmailUtils.IsValidEmail(clienteDTO.Email).Equals(true);
            PhoneUtils.IsValidPhoneNumber(clienteDTO.Telefone).Equals(false);

            // Act & Assert
            Xunit.Assert.Throws<ValidationException>(() => _clienteUseCase.Save(clienteDTO));
        }

        [Fact]
        public void Save_ShouldThrowValidationException_WhenDataNascimentoIsInvalid()
        {
            // Arrange
            var clienteDTO = new CreateClienteDTO
            {
                Nome = "Cliente Teste",
                Cpf = "12345678909",
                Email = "cliente@teste.com",
                Telefone = "11999999999",
                DataNascimento = DateTime.Now.AddDays(1) // Data de nascimento futura
            };

            _clienteRepositoryMock.Setup(repo => repo.GetByDocument(It.IsAny<string>())).Returns(new Cliente());
            CpfUtils.IsValidCpf(clienteDTO.Cpf).Equals(true);
            EmailUtils.IsValidEmail(clienteDTO.Email).Equals(true);
            PhoneUtils.IsValidPhoneNumber(clienteDTO.Telefone).Equals(true);

            // Act & Assert
            Xunit.Assert.Throws<ValidationException>(() => _clienteUseCase.Save(clienteDTO));
        }

        [Fact]
        public void GetById_ShouldReturnClienteDTO_WhenClienteExists()
        {
            // Arrange
            var cliente = new Cliente
            {
                Id = 1,
                Nome = "Cliente Teste",
                Cpf = "12345678909",
                Email = "cliente@teste.com",
                Telefone = "11999999999",
                DataNascimento = new DateTime(2000, 1, 1)
            };

            _clienteRepositoryMock.Setup(repo => repo.GetById(1)).Returns(cliente);

            // Act
            var result = _clienteUseCase.GetById(1);

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(cliente.Nome, result.Nome);
        }

        [Fact]
        public void GetByDocument_ShouldReturnClienteDTO_WhenClienteExists()
        {
            // Arrange
            var cliente = new Cliente
            {
                Id = 1,
                Nome = "Cliente Teste",
                Cpf = "12345678909",
                Email = "cliente@teste.com",
                Telefone = "11999999999",
                DataNascimento = new DateTime(2000, 1, 1)
            };

            _clienteRepositoryMock.Setup(repo => repo.GetByDocument(It.IsAny<string>())).Returns(cliente);

            // Act
            var result = _clienteUseCase.GetByDocument("12345678909");

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(cliente.Nome, result.Nome);
        }
    }
}