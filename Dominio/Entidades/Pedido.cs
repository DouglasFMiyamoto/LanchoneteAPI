using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entidades
{
    public class Pedido
    {
        public Pedido()
        {
            Itens = new HashSet<PedidoItem>();
        }

        public Pedido(int clienteId, PedidoStatus stauts, ICollection<PedidoItem> itens, DateTime dataCriacao, decimal valor)
        {
            ClienteId = clienteId;
            Status = stauts;
            Itens = itens ?? new List<PedidoItem>();
            DataCriacao = dataCriacao;
            Valor = valor;
        }

        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int ClienteId { get; set; }
        [Required]
        public PedidoStatus Status { get; set; }
        [Required]
        public ICollection<PedidoItem> Itens { get; set; }
        [Required]
        public DateTime DataCriacao { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [ForeignKey("ClienteId")]
        public virtual Cliente? Cliente { get; set; }
    }
}
