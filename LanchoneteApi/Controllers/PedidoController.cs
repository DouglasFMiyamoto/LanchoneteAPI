using Application.DTOs;
using Application.UseCases;
using Application.Utils;
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

        /// <summary>
        /// Construtor 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="pedidoUseCase"></param>
        public PedidoController(ILogger<PedidoController> logger, IPedidoUseCase pedidoUseCase)
        {
            _logger = logger;
            _pedidoUseCase = pedidoUseCase;
        }

        /// <summary>
        /// Salva o registro de um pedido no banco de dados
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso o registro seja salvo com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Save([FromBody] CreatePedidoDTO pedido)
        {
            try
            {
                _pedidoUseCase.Save(pedido);
                return Ok(pedido);
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
    }
}
