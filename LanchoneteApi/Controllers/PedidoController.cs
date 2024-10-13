using Application.DTOs;
using Application.UseCases;
using Application.Utils;
using LanchoneteApi.Presenters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LanchoneteApi.Controllers
{
    /// <summary>
    /// Controller de pedido
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly IPedidoUseCase _pedidoUseCase;
        private readonly ICheckoutPedidoUseCase _checkoutPedidoUseCase;
        private readonly ICheckoutPedidoPresenter _checkoutPresenter;

        /// <summary>
        /// Construtor 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="pedidoUseCase"></param>
        /// <param name="checkoutPedidoUseCase"></param>
        /// <param name="checkoutPresenter"></param>
        public PedidoController(ILogger<PedidoController> logger, 
                                IPedidoUseCase pedidoUseCase,
                                ICheckoutPedidoUseCase checkoutPedidoUseCase,
                                ICheckoutPedidoPresenter checkoutPresenter)
        {
            _logger = logger;
            _pedidoUseCase = pedidoUseCase;
            _checkoutPedidoUseCase = checkoutPedidoUseCase;
            _checkoutPresenter = checkoutPresenter;
        }

        /// <summary>
        /// Salva o registro de um pedido no banco de dados e retorna o seu identificador
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso o registro seja salvo com sucesso</response>
        [HttpPost("checkout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckoutPedido([FromBody] CreatePedidoDTO pedido)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var pedidoId = await _checkoutPedidoUseCase.ExecutarAsync(pedido);

                // Use o Presenter para preparar a resposta
                var response = _checkoutPresenter.PrepareResponse(pedidoId);

                return Ok(response);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(new ErrorResponse { Message = "Validation error", Details = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                return StatusCode(500, new ErrorResponse { Message = "An unexpected error occurred", Details = ex.Message });
            }
        }

        /// <summary>
        /// Recupera uma lista de pedidos
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a lista de pedido seja retornada com sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            try
            {
                var pedidos = _pedidoUseCase.GetAll();
                return Ok(pedidos);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(new ErrorResponse { Message = "Validation error", Details = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                return StatusCode(500, new ErrorResponse { Message = "An unexpected error occurred", Details = ex.Message });
            }
        }

        /// <summary>
        /// Consulta o status do pagamento do pedido
        /// </summary>
        /// <param name="pedidoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{pedidoId}/status-pagamento")]
        public async Task<IActionResult> ConsultarStatusPagamento(int pedidoId)
        {
            try
            {
                var statusPedido = await _checkoutPedidoUseCase.ConsultarStatusPagamento(pedidoId);

                var statusPagamento = new
                {
                    PedidoId = pedidoId,
                    StatusPagamento = statusPedido.ToString()
                };

                return Ok(statusPagamento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao consultar status do pagamento", error = ex.Message });
            }
        }

        /// <summary>
        /// Retorna a lista de pedidos ordenada por data e status
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getPedidosOrdenadosPorStatusEData")]
        public async Task<IActionResult> GetPedidosOrdenadosPorStatusEData()
        {
            try
            {
                var pedidos = await _pedidoUseCase.GetPedidosOrdenadosPorStatusEDataAsync();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao listar pedidos", error = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza o status do pedido: 0 - Recebido, 1 - EmPreparação, 2 - Pronto, 3 - Finalizado, 4 - Recusado
        /// </summary>
        /// <param name="pedidoId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{pedidoId}/atualizar-status")]
        public async Task<IActionResult> AtualizarStatus(int pedidoId, [FromBody] AtualizarStatusPedidoRequest request)
        {
            try
            {
                await _pedidoUseCase.AtualizarStatusPedido(pedidoId, request.Status);
                return Ok(new { message = "Status do pedido atualizado com sucesso" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao atualizar status do pedido", error = ex.Message });
            }
        }
    }
}
