namespace Application.DTOs
{
    public class PaymentWebhookRequest
    {
        public string PagamentoStatus { get; set; } = string.Empty;
        public int PedidoId { get; set; }
        public decimal ValorPago { get; set; }
        public string TransacaoId { get; set; } = string.Empty;
    }
}
