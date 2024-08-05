using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class CreatePedidoItemDTO
    {
        [Required]
        public int ProdutoId { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [StringLength(200, ErrorMessage = "A customização não deve exceder 200 caracteres")]
        public string Customizacao { get; set; } = string.Empty;
    }
}
