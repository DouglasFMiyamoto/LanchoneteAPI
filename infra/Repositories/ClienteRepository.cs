using Application.Repository;
using Domain.Entidades;
using Lanchonete.infrastructure.Data;

namespace Lanchonete.infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public void Save(Cliente cliente)
        {
            try
            {
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao salvar o cliente com o CPF: {cliente.Cpf}", ex);
            }

        }

        /// <inheritdoc/>
        public Cliente? GetById(int id)
        {
            try
            {
                return _context.Clientes.FirstOrDefault(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível encontrar o cliente com o Id: {id}", ex);
            }
        }

        /// <inheritdoc/>
        public Cliente? GetByDocument(string document)
        {
            try
            {
                return _context.Clientes.FirstOrDefault(c => c.Cpf == document);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível encontrar o cliente com o CPF: {document}", ex);
            }
        }
    }
}
