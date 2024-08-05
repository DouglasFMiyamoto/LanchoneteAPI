using Application.Repository;
using Dominio.Entidades;
using infra.Data;

namespace infra.Repository
{
    public class PedidoItemRepository : IPedidoItemRepository
    {
        private ApplicationDbContext _context;

        public PedidoItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public IList<PedidoItem>? GetByPedidoId(int pedidoId)
        {
            try
            {
                return _context.PedidoItem.Where(p => p.PedidoId == pedidoId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível encontrar o item do pedido para o Id do pedido: {pedidoId}", ex);
            }
        }
    }
}
