using Domain.Entidades;

namespace Application.Repository
{
    public interface ICategoriaRepository
    {
        /// <summary>
        /// Método responsável por recuperar as categorias de produto
        /// </summary>
        /// <returns></returns>
        IList<Categoria>? GetAll();
    }
}
