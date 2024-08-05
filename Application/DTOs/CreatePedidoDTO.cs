using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class CreatePedidoDTO
    {
        [Required]
        public int ClienteId { get; set; }
        [Required]
        public IList<CreatePedidoItemDTO>? Itens { get; set; }
    }
}
