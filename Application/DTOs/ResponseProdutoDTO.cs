namespace Application.DTOs
{
    public class ResponseProdutoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public int Estoque { get; set; }
        public bool Disponivel { get; set; }
        public int CategoriaId { get; set; }
        public int OrdemExibicao { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
