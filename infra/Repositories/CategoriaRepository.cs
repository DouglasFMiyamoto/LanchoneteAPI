using Application.Repository;
using Domain.Entidades;
using Lanchonete.infrastructure.Data;

namespace Lanchonete.infrastructure.Repositories
{
    public class CategoriaRepository: ICategoriaRepository
    {
        private ApplicationDbContext _context;

        public CategoriaRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        /// <inheritdoc/>
        public IList<Categoria>? GetAll()
        {
            try
            {
                return _context.Categorias.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível recuperar a lista de categorias.", ex);
            }
        }
    }
}
