using Domain.Entidades;

namespace Application.Repository
{
    public interface IClienteRepository
    {
        /// <summary>
        /// Método que salva o cliente no banco de dados
        /// </summary>
        /// <param name="cliente"></param>
        void Save(Cliente cliente);
        /// <summary>
        /// Método que retorna o cliente pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Cliente? GetById(int id);
        /// <summary>
        /// Método que retorna o cliente pelo cpf
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        Cliente? GetByDocument(string document);
    }
}
