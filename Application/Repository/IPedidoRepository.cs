using Dominio.Entidades;

namespace Application.Repository
{
    public interface IPedidoRepository
    {
        /// <summary>
        /// Método responsável salvar o registro do pedido na base de dados
        /// </summary>
        /// <param name="pedido"></param>
        void Save(Pedido pedido);
        /// <summary>
        /// Método responsável por recuperar todos os pedidos cadastrados
        /// </summary>
        /// <returns></returns>
        IList<Pedido>? GetAll();
    }
}
