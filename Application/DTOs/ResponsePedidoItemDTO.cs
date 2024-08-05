namespace Application.DTOs
{
    public class ResponsePedidoItemDTO
    {
        public ResponsePedidoItemDTO()
        {
            Produto = new ResponseProdutoDTO();
        }

        public int Id { get; set; }
        public ResponseProdutoDTO Produto { get; set; }
        public int Quantidade { get; set; }
        public string Customizacao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
