using Dominio.Entidades;

namespace Application.Repository
{
    public interface IPedidoItemRepository
    {
        /// <summary>
        /// Método responsável por retornar os items do pedido de acordo com o Id do pedido
        /// </summary>
        /// <param name="pedidoId"></param>
        /// <returns></returns>
        IList<PedidoItem>? GetByPedidoId(int pedidoId);
    }
}
