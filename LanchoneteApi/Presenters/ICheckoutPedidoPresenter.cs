using Application.DTOs;

namespace LanchoneteApi.Presenters
{
    /// <summary>
    /// Presenter da criação do pedido
    /// </summary>
    public interface ICheckoutPedidoPresenter
    {
        /// <summary>
        /// Retorna a resposta da criação do pedido
        /// </summary>
        /// <param name="pedidoId"></param>
        /// <returns></returns>
        CheckoutPedidoResponse PrepareResponse(int? pedidoId);
    }
}
