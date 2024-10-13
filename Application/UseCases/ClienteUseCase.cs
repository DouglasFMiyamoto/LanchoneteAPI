using Application.DTOs;
using Application.Repository;
using Application.Utils;
using AutoMapper;
using Domain.Entidades;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.UseCases
{
    public class ClienteUseCase : IClienteUseCase
    {
        public readonly IClienteRepository _clienteRepository;
        private IMapper _mapper;

        public ClienteUseCase(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public void Save(CreateClienteDTO clienteDTO)
        {            
            Cliente cliente = _mapper.Map<Cliente>(clienteDTO);
            cliente.DataCriacao = System.DateTime.Now;
         
            ValidaCliente(cliente);

            _clienteRepository.Save(cliente);
        }
        /// <inheritdoc/>
        public ResponseClienteDTO? GetById(int id)
        {
            Cliente? cliente = _clienteRepository.GetById(id);
            return _mapper.Map<ResponseClienteDTO>(cliente);
        }
        /// <inheritdoc/>
        public ResponseClienteDTO? GetByDocument(string document)
        {
            Cliente? cliente = _clienteRepository.GetByDocument(StringUtils.RemoverCaracteresEspeciais(document));            
            return _mapper.Map<ResponseClienteDTO>(cliente);
        }

        private void ValidaCliente(Cliente cliente)
        {
            var existsCliente = GetByDocument(cliente.Cpf);

            if (existsCliente != null)
                throw new ValidationException(new StringBuilder("Já existe um cliente cadastrado com o CPF: ").Append(existsCliente.Cpf).ToString());

            if (!CpfUtils.IsValidCpf(cliente.Cpf))
                throw new ValidationException(new StringBuilder("O CPF: ").Append(cliente.Cpf).Append(" é inválido").ToString());

            if (!EmailUtils.IsValidEmail(cliente.Email))
                throw new ValidationException(new StringBuilder("O email: ").Append(cliente.Email).Append(" é inválido").ToString());

            if (!PhoneUtils.IsValidPhoneNumber(cliente.Telefone))
                throw new ValidationException(new StringBuilder("O telefone: ").Append(cliente.Telefone).Append(" é inválido").ToString());

            if (!IsValidDataNascimento(cliente.DataNascimento))
                throw new ValidationException("A data de nascimento é inválida.");
        }

        private bool IsValidDataNascimento(DateTime dataNascimento)
        {
            if (dataNascimento > DateTime.Now)
            {
                return false;
            }

            return true;
        }
    }
}
