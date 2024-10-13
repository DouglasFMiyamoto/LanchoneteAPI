using Application.DTOs;
using Domain.Enums;

namespace Application.UseCases
{
    public interface IPedidoUseCase
    {
        /// <summary>
        /// Método responsável por recuperar todos os pedidos
        /// </summary>
        /// <returns></returns>
        IList<ResponsePedidoDTO> GetAll();

        /// <summary>
        /// Retorna a lista de pedidos ordenada por data e status
        /// </summary>
        /// <returns></returns>
        Task<IList<ResponsePedidoDTO>> GetPedidosOrdenadosPorStatusEDataAsync();


        /// <summary>
        /// Atualiza o status do pedido
        /// </summary>
        /// <param name="pedidoId"></param>
        /// <param name="status"></param>
        Task<bool>AtualizarStatusPedido(int pedidoId, PedidoStatus status);
    }
}
