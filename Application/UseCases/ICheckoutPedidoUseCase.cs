using Application.DTOs;
using Domain.Enums;

namespace Application.UseCases
{
    public interface ICheckoutPedidoUseCase
    {
        /// <summary>
        /// Registra o pedido
        /// </summary>
        /// <param name="createPedidoDTO"></param>
        /// <returns></returns>
        Task<int?> ExecutarAsync(CreatePedidoDTO createPedidoDTO);

        /// <summary>
        /// Confirma o pagamento e atualiza o status do pedido
        /// </summary>
        /// <param name="pedidoId"></param>
        /// <param name="isAprovado"></param>
        /// <returns></returns>
        Task ConfirmarPagamento(int pedidoId, bool isAprovado);

        /// <summary>
        /// Consulta o status de pagamento do pedido
        /// </summary>
        /// <param name="pedidoId"></param>
        /// <returns></returns>
        Task<PagamentoStatus> ConsultarStatusPagamento(int pedidoId);
    }
}
