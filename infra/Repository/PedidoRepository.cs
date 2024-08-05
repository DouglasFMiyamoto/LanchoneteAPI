using Application.Repository;
using Dominio.Entidades;
using infra.Data;
using System.Text;

namespace infra.Repository
{
    public class PedidoRepository : IPedidoRepository 
    {
        private ApplicationDbContext _context;

        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public void Save(Pedido pedido)
        {
            try
            {
                _context.Pedidos.Add(pedido);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(new StringBuilder("Erro ao salvar o pedido na base de dados").ToString(), ex);
            }
        }

        /// <inheritdoc/>
        public IList<Pedido>? GetAll()
        {
            try
            {
                return _context.Pedidos.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível recuperar a lista de pedidos.", ex);
            }
        }
    }
}
