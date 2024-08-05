using Application.Repository;
using Dominio.Entidades;
using infra.Data;
using System.Text;

namespace infra.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public void Save(Produto produto)
        {
            try
            {
                _context.Produtos.Add(produto);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(new StringBuilder("Erro ao salvar o produto na base de dados").ToString(), ex);
            }
        }

        /// <inheritdoc/>
        public void Update(Produto produto)
        {
            try
            {
                _context.Produtos.Update(produto);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(new StringBuilder("Erro ao atualizar o produto na base de dados").ToString(), ex);
            }
        }

        /// <inheritdoc/>
        public Produto? GetById(int id)
        {
            try
            {
                return _context.Produtos.FirstOrDefault(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível encontrar o produto com o Id: {id}", ex);
            }
        }

        /// <inheritdoc/>
        public IList<Produto>? GetAll()
        {
            try
            {
                return _context.Produtos.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível recuperar a lista de produtos.", ex);
            }
        }

        /// <inheritdoc/>
        public IList<Produto>? GetByCategory(int categoriaId)
        {
            try
            {
                return _context.Produtos.Where(p => p.CategoriaId == categoriaId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível encontrar o produto com o Id de categoria: {categoriaId}", ex);
            }
        }

        /// <inheritdoc/>
        public void Update(int id, Produto produto)
        {
            try
            {
                var produtoUpdate = _context.Produtos.Find(id);

                if (produtoUpdate == null)
                    throw new Exception($"Produto com o Id {id} não foi encontrado.");

                produtoUpdate.Nome = produto.Nome;
                produtoUpdate.Descricao = produto.Descricao;
                produtoUpdate.Valor = produto.Valor;
                produtoUpdate.CategoriaId = produto.CategoriaId;
                produtoUpdate.OrdemExibicao = produto.OrdemExibicao;
                produtoUpdate.Estoque = produto.Estoque;
                produtoUpdate.Disponivel = produto.Disponivel;

                _context.Produtos.Update(produtoUpdate);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível atualizar o produto com o Id: {id}", ex);
            }
        }

        /// <inheritdoc/>
        public void Delete(int id)
        {
            try
            {
                var produto = _context.Produtos.Find(id);
                if (produto == null)
                {
                    throw new Exception($"Produto com o Id {id} não foi encontrado.");
                }

                _context.Produtos.Remove(produto);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível excluir o produto com o Id: {id}", ex);
            }
        }
    }
}
