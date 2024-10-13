namespace Application.DTOs
{
    public class CheckoutPedidoResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public int PedidoId { get; set; }
    }
}
