using Application.DTOs;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Application.Utils;

namespace LanchoneteApi.Controllers
{
    /// <summary>
    /// Controller de cliente
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IClienteUseCase _clienteUseCase;

        /// <summary>
        /// Construtor do controller de cliente
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="clienteUseCase"></param>
        public ClienteController(ILogger<ClienteController> logger, IClienteUseCase clienteUseCase)
        {
            _logger = logger;
            _clienteUseCase = clienteUseCase;
        }

        /// <summary>
        /// Salva o registro de um cliente no banco de dados
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso o registro seja salvo com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Save([FromBody] CreateClienteDTO cliente)
        {
            try
            {
                _clienteUseCase.Save(cliente);
                return Ok(cliente);
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
        /// Identifica um cliente através do CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso o registro seja identificado com sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetByDocument(string cpf)
        {
            try
            {
                var cliente = _clienteUseCase.GetByDocument(cpf);

                if (cliente is null)
                    throw new ValidationException($"Cliente não identificado com o CPF: {cpf}");

                return Ok(cliente);
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
