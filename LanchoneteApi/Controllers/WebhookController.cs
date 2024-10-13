using Application.DTOs;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace LanchoneteApi.Controllers
{
    /// <summary>
    /// Controller responsável pelos webhooks
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        private readonly ILogger<WebhookController> _logger;
        private readonly ICheckoutPedidoUseCase _checkoutPedidoUseCase;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="checkoutPedidoUseCase"></param>
        public WebhookController(ILogger<WebhookController> logger, ICheckoutPedidoUseCase checkoutPedidoUseCase)
        {
            _logger = logger;
            _checkoutPedidoUseCase = checkoutPedidoUseCase;
        }

        /// <summary>
        /// Webhook de confirmação de pagamento
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("payment-confirmation")]
        public async Task<IActionResult> PaymentConfirmation([FromBody] PaymentWebhookRequest request)
        {
            try
            {
                if (request is null || string.IsNullOrEmpty(request.PagamentoStatus))
                {
                    return BadRequest("Dados inválidos recebidos.");
                }

                if (request.PagamentoStatus.ToUpper().Equals("APROVADO"))
                {
                    await _checkoutPedidoUseCase.ConfirmarPagamento(request.PedidoId, true);
                }
                else if (request.PagamentoStatus.ToUpper().Equals("RECUSADO"))
                {
                    await _checkoutPedidoUseCase.ConfirmarPagamento(request.PedidoId, false);
                }

                return Ok(new { message = "Webhook recebido e processado com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao processar o webhook", error = ex.Message });
            }
        }
    }
}
