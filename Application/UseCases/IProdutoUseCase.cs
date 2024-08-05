using Application.DTOs;

namespace Application.UseCases
{
    public interface IProdutoUseCase
    {
        /// <summary>
        /// Método responsável por acessar a infra de produto e salvar o registro na base de dados
        /// </summary>
        /// <param name="produtoDTO"></param>
        void Save(CreateProdutoDTO produtoDTO);
        /// <summary>
        /// Método responsável por atualizar o registro de produto na base de dados
        /// </summary>
        /// <param name="updateProdutoDTO"></param>
        /// <param name="id"></param>
        void Update(UpdateProdutoDTO updateProdutoDTO, int id);
        /// <summary>
        /// Método responsável por recuperar o produto através do Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResponseProdutoDTO? GetById(int id);
        /// <summary>
        /// Método responsável por recuperar todos os produtos
        /// </summary>
        /// <returns></returns>
        IList<ResponseProdutoDTO> GetAll();
        /// <summary>
        /// Método responsável por recuperar os produtos através da categoria
        /// </summary>
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        IList<ResponseProdutoDTO> GetByCategory(int categoryId);
        /// <summary>
        /// Método responsável por acessar a infra de produto e deletar o registro na base de dados
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }
}
