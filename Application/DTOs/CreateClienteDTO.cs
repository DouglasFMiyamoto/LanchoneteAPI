using System.ComponentModel.DataAnnotations;
using Application.Utils;

namespace Application.DTOs
{
    public class CreateClienteDTO
    {
        private string _cpf = string.Empty;
        private string _telefone = string.Empty;

        [Required(ErrorMessage = "É preciso informar o nome.")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais que 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "É preciso informar o CPF.")]
        [StringLength(11, ErrorMessage = "O CPF não pode ter mais que 11 caracteres.")]
        public string Cpf 
        { 
            get => _cpf; 
            set => _cpf = StringUtils.RemoverCaracteresEspeciais(value); 
        }
        [Required(ErrorMessage = "É preciso informar o email.")]
        [StringLength(100, ErrorMessage = "O email não pode ter mais que 100 caracteres.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "É preciso informar o telefone.")]
        [StringLength(11, ErrorMessage = "O telefone não pode ter mais que 11 caracteres.")]
        public string Telefone 
        {
            get => _telefone;
            set => _telefone = StringUtils.RemoverCaracteresEspeciais(value);
        } 
        [Required(ErrorMessage = "É preciso informar a data de nascimento.")]
        public DateTime DataNascimento { get; set; }
    }
}
