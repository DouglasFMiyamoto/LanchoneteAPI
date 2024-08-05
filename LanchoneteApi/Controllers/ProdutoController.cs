using Application.DTOs;
using Application.UseCases;
using Application.Utils;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LanchoneteApi.Controllers
{
    /// <summary>
    /// Controller de produto
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ILogger<ProdutoController> _logger;
        private readonly IProdutoUseCase _produtoUseCase;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="produtoUseCase"></param>
        public ProdutoController(ILogger<ProdutoController> logger, IProdutoUseCase produtoUseCase)
        {
            _logger = logger;
            _produtoUseCase = produtoUseCase;
        }

        /// <summary>
        /// Salva o registro de um produto no banco de dados
        /// Categorias: 1 - Lanche; 2 - Acompanhamento; 3 - Bebida; 4 - Sobremesa;
        /// </summary>
        /// <param name="produto"></param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso o registro seja salvo com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Save([FromBody] CreateProdutoDTO produto)
        {
            try
            {
                _produtoUseCase.Save(produto);
                return Ok(produto);
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
        /// Salva o registro de um produto no banco de dados
        /// Categorias: 1 - Lanche; 2 - Acompanhamento; 3 - Bebida; 4 - Sobremesa;
        /// </summary>
        /// <param name="id"></param>
        /// <param name="produto"></param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso o registro seja atualizado com sucesso</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Update(int id, [FromBody] UpdateProdutoDTO produto)
        {
            try
            {
                _produtoUseCase.Update(produto, id);
                return NoContent();
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
        /// Deleta o registro de um produto no banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso o registro seja deletado com sucesso</response>

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            try
            {
                _produtoUseCase.Delete(id);
                return NoContent();
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
        /// Recupera o registro de um produto através do id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso o registro seja retornado com sucesso</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetById(int id)
        {
            try
            {
                var produto = _produtoUseCase.GetById(id);

                if (produto is null)
                    throw new ValidationException($"Produto não identificado com o Id: {id}");

                return Ok(produto);
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
        /// Recupera uma lista de produtos
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a lista de produto seja retornada com sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            try
            {
                var produtos = _produtoUseCase.GetAll();
                return Ok(produtos);
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
        /// Recupera uma lista de produtos através do id de categoria
        /// Categorias: 1 - Lanche; 2 - Acompanhamento; 3 - Bebida; 4 - Sobremesa;
        /// </summary>
        /// <param name="categoriaId"></param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a lista de produto seja retornada com sucesso</response>
        [HttpGet("getByCategory/{categoriaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetByCategory(int categoriaId)
        {
            try
            {
                var produtos = _produtoUseCase.GetByCategory(categoriaId);

                if (produtos is null || !produtos.Any())
                    throw new ValidationException($"Não identificamos produtos com o Id de categoria: {categoriaId}");

                return Ok(produtos);
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
