using Application.DTOs;

namespace LanchoneteApi.Presenters
{
    /// <summary>
    /// Formata as mensagens de resposta do checkout
    /// </summary>
    public class CheckoutPedidoPresenter : ICheckoutPedidoPresenter
    {
        /// <inheritdoc/>
        public CheckoutPedidoResponse PrepareResponse(int? pedidoId)
        {
            if (pedidoId == null)
            {
                return new CheckoutPedidoResponse
                {
                    Sucesso = false,
                    Mensagem = "Erro ao processar o pedido."
                };
            }

            return new CheckoutPedidoResponse
            {
                Sucesso = true,
                PedidoId = pedidoId.Value,
                Mensagem = "Pedido criado com sucesso!"
            };
        }
    }
}
