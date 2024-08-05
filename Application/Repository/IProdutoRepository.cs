using Dominio.Entidades;

namespace Application.Repository
{
    public interface IProdutoRepository
    {
        /// <summary>
        /// Método responsável por salvar o registro de produto na base de dados
        /// </summary>
        /// <param name="produto"></param>
        void Save(Produto produto);
        /// <summary>
        /// Método responsável por atualizar o registro de produto na base de dados
        /// </summary>
        /// <param name="produto"></param>
        void Update(Produto produto);
        /// <summary>
        /// Método responsável por recuperar o produto através do Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Produto? GetById(int id);
        /// <summary>
        /// Método responsável por recuperar todos os produtos cadastrados
        /// </summary>
        /// <returns></returns>
        IList<Produto>? GetAll();
        /// <summary>
        /// Método responsável por recuperar os produtos através do Id da categoria
        /// </summary>
        /// <param name="categoriaId"></param>
        /// <returns></returns>
        IList<Produto>? GetByCategory(int categoriaId);
        /// <summary>
        /// Método responsável por atualizar o produto na base de dados
        /// </summary>
        /// <param name="id"></param>
        ///  <param name="produto"></param>
        void Update(int id, Produto produto);
        /// <summary>
        /// Método responsável por deletar um produto na base de dados
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }
}
