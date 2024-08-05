using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public class Cliente
    {
        public Cliente() { }

        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;
        [Required]
        [MaxLength(11)]
        public string Cpf { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(11)]
        public string Telefone { get; set; } = string.Empty;
        [Required]     
        public DateTime DataNascimento { get; set; } 
        [Required]
        public DateTime DataCriacao { get; set; }
    }
}
