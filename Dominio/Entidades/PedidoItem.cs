using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    public class PedidoItem
    {
        public PedidoItem() { }

        [Key]
        public int Id { get; set; }
        [Required]
        public int PedidoId { get; set; }
        [Required]
        public int ProdutoId { get; set; }
        [Required]
        [MaxLength(30)]
        public string NomeProduto { get; set; } = string.Empty;
        [Required]
        public int Quantidade { get; set; }
        [MaxLength(200)]
        public string Customizacao { get; set; } = string.Empty;
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public DateTime DataCriacao { get; set; }

        [ForeignKey("PedidoId")]
        public virtual Pedido? Pedido { get; set; }
        [ForeignKey("ProdutoId")]
        public virtual Produto? Produto { get; set; }
    }
}
