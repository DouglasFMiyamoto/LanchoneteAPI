using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entidades
{
    public class Produto
    {
        public Produto() { }
        public Produto(string nome, string descricao, decimal valor, int estoque, bool disponivel, int categoriaId, int ordemExibicao)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            Estoque = estoque;
            Disponivel = disponivel;
            CategoriaId = categoriaId;
            OrdemExibicao = ordemExibicao;
        }

        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nome { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Descricao { get; set; } = string.Empty;
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public int Estoque { get; set; }
        [Required]
        public bool Disponivel { get; set; }
        [Required]
        public int CategoriaId { get; set; }
        [Required]
        public int OrdemExibicao { get; set; }
        [Required]
        public DateTime DataCadastro { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual Categoria? Categoria { get; set; }
    }
}
