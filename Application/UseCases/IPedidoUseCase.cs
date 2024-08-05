using Application.DTOs;

namespace Application.UseCases
{
    public interface IPedidoUseCase
    {
        /// <summary>
        /// Método responsável por acessar a infra de pedido e salvar o registro na base de dados
        /// </summary>
        /// <param name="pedidoDTO"></param>
        void Save(CreatePedidoDTO pedidoDTO);
        /// <summary>
        /// Método responsável por recuperar todos os pedidos
        /// </summary>
        /// <returns></returns>
        IList<ResponsePedidoDTO> GetAll();
    }
}
