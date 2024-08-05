using Application.DTOs;

namespace Application.UseCases
{
    public interface IClienteUseCase
    {
        /// <summary>
        /// Método responsável por acessar a infra de cliente e salvar o registro na base de dados
        /// </summary>
        /// <param name="clienteDTO"></param>
        void Save(CreateClienteDTO clienteDTO);
        /// <summary>
        /// Método responsável por acessar a infra de cliente e retorna 1 cliente através do id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResponseClienteDTO? GetById(int id);
        /// <summary>
        /// Método responsável por acessar a infra de cliente e retorna 1 cliente através do cpf
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        ResponseClienteDTO? GetByDocument(string document);
    }
}
