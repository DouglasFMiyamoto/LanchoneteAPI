using Domain.Entidades;

namespace Application.Repository
{
    public interface IPedidoRepository
    {
        /// <summary>
        /// Método responsável por salvar o registro do pedido na base de dados
        /// </summary>
        /// <param name="pedido"></param>
        void Save(Pedido pedido);
        /// <summary>
        /// Método responsável por recuperar todos os pedidos cadastrados
        /// </summary>
        /// <returns></returns>
        IList<Pedido>? GetAll();

        /// <summary>
        /// Método responsável por salvar o registro do pedido na base de dados, retorna o identificador do pedido
        /// </summary>
        /// <param name="pedido"></param>
        Task<int> CriarPedidoAsync(Pedido pedido);

        /// <summary>
        /// Retorana o pedido pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Pedido?> GetByIdAsync(int id);

        /// <summary>
        /// Atualiza o pedido
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        Task UpdateAsync(Pedido pedido);

        /// <summary>
        /// Retorna a lista de pedidos ordenada por data e status
        /// </summary>
        /// <returns></returns>
        Task<IList<Pedido>> GetPedidosOrdenadosPorStatusEDataAsync();
    }
}
