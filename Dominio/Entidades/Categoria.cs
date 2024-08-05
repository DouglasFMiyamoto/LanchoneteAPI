using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public class Categoria
    {
        public Categoria()
        {
            Produtos = new HashSet<Produto>();
        }

        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nome { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Descricao { get; set; } = string.Empty;
        [Required]
        public int Ordem { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}
