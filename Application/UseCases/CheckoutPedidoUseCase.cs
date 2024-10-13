using Application.DTOs;
using Application.Repository;
using Domain.Entidades;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.UseCases
{
    public class CheckoutPedidoUseCase : ICheckoutPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        public readonly IClienteRepository _clienteRepository;
        public readonly IProdutoRepository _produtoRepository;

        public CheckoutPedidoUseCase(IPedidoRepository pedidoRepository, 
                                     IClienteRepository clienteRepository, 
                                     IProdutoRepository produtoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
        }

        /// <inheritdoc/>
        public async Task<int?> ExecutarAsync(CreatePedidoDTO createPedidoDTO)
        {
            if (!IsValidCliente(createPedidoDTO.ClienteId))
                throw new ValidationException("Cliente não encontrado");

            if (createPedidoDTO.Itens is null || !createPedidoDTO.Itens.Any())
                throw new ValidationException("O pedido não contém nenhum item");

            var pedido = new Pedido
            {
                ClienteId = createPedidoDTO.ClienteId,
                Status = PedidoStatus.Recebido,
                DataCriacao = DateTime.Now,
                Valor = 0,//itens.Sum(i => i.Valor),
                Itens = createPedidoDTO.Itens.Select(p => new PedidoItem(p.ProdutoId, p.Quantidade, p.Customizacao, GetProduto(p.ProdutoId))).ToList(), 
            };

            var pedidoId = await _pedidoRepository.CriarPedidoAsync(pedido);

            return pedidoId;
        }

        /// <inheritdoc/>
        public async Task ConfirmarPagamento(int pedidoId, bool isAprovado)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(pedidoId);

            if (pedido is null)
                throw new Exception("Pedido não encontrado");

            if (isAprovado)            
                pedido.Status = PedidoStatus.EmPreparacao;            
            else            
                pedido.Status = PedidoStatus.Recusado;
            
            await _pedidoRepository.UpdateAsync(pedido);
        }

        /// <inheritdoc/>
        public async Task<PagamentoStatus> ConsultarStatusPagamento(int pedidoId)
        {
            var retorno = PagamentoStatus.Aprovado;
            var pedido = await _pedidoRepository.GetByIdAsync(pedidoId);

            if (pedido is null)
                throw new Exception("Pedido não encontrado");

            if (pedido.Status == PedidoStatus.Recusado)
                retorno = PagamentoStatus.Recusado;

            if (pedido.Status == PedidoStatus.Recebido)
                retorno = PagamentoStatus.Pendente;

            return retorno;
        }

        private Produto GetProduto(int produtoId)
        {
            var produto = _produtoRepository.GetById(produtoId);

            if (produto is null)
                throw new ValidationException("Produto não encontrado");

            return produto;
        }

        private bool IsValidCliente(int clienteId)
        {
            var cliente = _clienteRepository.GetById(clienteId);
            return cliente != null;
        }
    }
}
