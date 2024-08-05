namespace Application.DTOs
{
    public class ResponsePedidoDTO
    {
        public ResponsePedidoDTO()
        {
            CreateClienteDTO cliente = new CreateClienteDTO();
            Itens = new List<ResponsePedidoItemDTO>();
        }

        public int Id { get; set; }
        public ResponseClienteDTO? Cliente { get; set; }
        public string Status { get; set; } = string.Empty;
        public IList<ResponsePedidoItemDTO> Itens { get; set; }
        public DateTime DataCriacao { get; set; }
        public decimal Valor { get; set; }
    }
}
