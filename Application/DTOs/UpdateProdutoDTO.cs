using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class UpdateProdutoDTO
    {
        [Required(ErrorMessage = "É necessário informar o nome do produto")]
        [StringLength(30, ErrorMessage = "O nome do produto não pode ter mais de 30 caracteres")]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "É necessário informar a descrição do produto")]
        [StringLength(100, ErrorMessage = "A descrição do produto não pode ter mais de 100 caracteres")]
        public string Descricao { get; set; } = string.Empty;
        [Required(ErrorMessage = "É necessário informar o valor do produto")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "É necessário informar o estoque do produto")]
        public int Estoque { get; set; }
        [Required(ErrorMessage = "É necessário informar a disponibilidade do produto")]
        public bool Disponivel { get; set; }
        [Required(ErrorMessage = "É necessário informar a categoria do produto")]
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "É necessário informar a ordem de exibição do produto")]
        public int OrdemExibicao { get; set; }
    }
}
