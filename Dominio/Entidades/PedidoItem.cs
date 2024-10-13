namespace Domain.Entidades
{
    public class PedidoItem
    {
        public PedidoItem() { }

        public PedidoItem(int produtoId, int quantidade, string customizacao, Produto produto)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
            Produto = produto;
            Valor = CalculaValor();
            DataCriacao = DateTime.UtcNow;
            Customizacao = customizacao;
        }

        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public string Customizacao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime DataCriacao { get; set; }
        public virtual Pedido? Pedido { get; set; }
        public virtual Produto? Produto { get; set; }

        public decimal CalculaValor()
        {
            if (Produto is null)
            {
                throw new InvalidOperationException("Produto não pode ser nulo ao calcular o valor.");
            }

            return Produto.Valor * Quantidade;
        }
    }
}
