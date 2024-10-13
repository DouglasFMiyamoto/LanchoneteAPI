using Application.Repository;
using Domain.Entidades;
using Domain.Enums;
using Lanchonete.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Lanchonete.infrastructure.Repositories
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
        public async Task<int> CriarPedidoAsync(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido.Id;
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

        /// <inheritdoc/>
        public async Task<Pedido?> GetByIdAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<IList<Pedido>> GetPedidosOrdenadosPorStatusEDataAsync()
        {
            return await _context.Pedidos
                .Where(p => p.Status != PedidoStatus.Finalizado && p.Status != PedidoStatus.Recusado)
                .OrderBy(p => p.Status == PedidoStatus.Pronto ? 1 :
                              p.Status == PedidoStatus.EmPreparacao ? 2 :
                              p.Status == PedidoStatus.Recebido ? 3 : 4) 
                .ThenBy(p => p.DataCriacao)
                .ToListAsync();
        }
    }
}
