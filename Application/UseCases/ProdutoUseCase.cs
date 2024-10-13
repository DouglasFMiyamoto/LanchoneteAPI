using Application.DTOs;
using Application.Repository;
using AutoMapper;
using Domain.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Application.UseCases
{
    public class ProdutoUseCase : IProdutoUseCase
    {
        public readonly IProdutoRepository _produtoRepository;
        public readonly ICategoriaRepository _categoriaRepository;
        private IMapper _mapper;

        public ProdutoUseCase(IProdutoRepository produtoRepository, IMapper mapper, ICategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _categoriaRepository = categoriaRepository;
        }

        /// <inheritdoc/>
        public void Save(CreateProdutoDTO produtoDTO)
        {
            Produto produto = _mapper.Map<Produto>(produtoDTO);
            ValidaProduto(produto);

            produto.DataCadastro = DateTime.Now;
            _produtoRepository.Save(produto);
        }

        /// <inheritdoc/>
        public void Update(UpdateProdutoDTO produtoDTO, int id)
        {
            var produto = _produtoRepository.GetById(id);
            Produto newProduto = _mapper.Map<Produto>(produtoDTO);

            ValidaProduto(newProduto);

            produto.Nome = newProduto.Nome;
            produto.Descricao = newProduto.Descricao;
            produto.Valor = newProduto.Valor;
            produto.Disponivel = newProduto.Disponivel;
            produto.CategoriaId = newProduto.CategoriaId;
            produto.Estoque = newProduto.Estoque;
            produto.OrdemExibicao = newProduto.OrdemExibicao;

            _produtoRepository.Update(produto);
        }

        /// <inheritdoc/>
        public ResponseProdutoDTO? GetById(int id)
        {
            Produto? produto = _produtoRepository.GetById(id);
            return _mapper.Map<ResponseProdutoDTO>(produto);
        }

        /// <inheritdoc/>
        public IList<ResponseProdutoDTO> GetAll()
        {
            List<Produto> produtos = _produtoRepository.GetAll()?.OrderBy(x => x.CategoriaId)
                                                                 .OrderBy(y => y.OrdemExibicao)
                                                                    .ToList() ?? new List<Produto>();
            return _mapper.Map<IList<ResponseProdutoDTO>>(produtos);
        }

        /// <inheritdoc/>
        public IList<ResponseProdutoDTO> GetByCategory(int categoryId)
        {
            List<Produto> produtos = _produtoRepository.GetByCategory(categoryId)?.OrderBy(x => x.OrdemExibicao).ToList()
                                        ?? new List<Produto>();
            return _mapper.Map<IList<ResponseProdutoDTO>>(produtos);
        }

        public void Delete(int id)
        {
            _produtoRepository.Delete(id);
        }

        private void ValidaProduto(Produto? produto)
        {
            if (produto is null)
                throw new ValidationException($"O produto não foi encontrado na base de dados");

            if (produto.Valor <= 0)
                throw new ValidationException("O valor do produto não pode ser menor que 0");

            if (produto.OrdemExibicao <= 0)
                throw new ValidationException("A ordem de exibição do produto não pode ser menor que 0");

            var categorias = _categoriaRepository.GetAll() ?? new List<Categoria>();
            if (!categorias.Any(c => c.Id == produto.CategoriaId))
                throw new ValidationException("A categoria informada não está cadastrada na base de dados");
        }
    }
}
