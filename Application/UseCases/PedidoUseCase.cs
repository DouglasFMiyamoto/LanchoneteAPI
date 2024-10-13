using Application.DTOs;
using Application.Repository;
using AutoMapper;
using Domain.Entidades;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.UseCases
{
    public class PedidoUseCase : IPedidoUseCase
    {
        public readonly IPedidoRepository _pedidoRepository;
        public readonly IPedidoItemRepository _pedidoItemRepository;
        public readonly IClienteRepository _clienteRepository;
        public readonly IProdutoRepository _produtoRepository;
        private IMapper _mapper;

        public PedidoUseCase(IPedidoRepository pedidoRepository,
                             IProdutoRepository produtoRepository,
                             IPedidoItemRepository pedidoItemRepository,
                             IClienteRepository clienteRepository,
                             IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
            _pedidoItemRepository = pedidoItemRepository;
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public IList<ResponsePedidoDTO> GetAll()
        {
            List<Pedido> pedidos = _pedidoRepository.GetAll()?.OrderBy(x => x.DataCriacao).ToList() ?? new List<Pedido>();

            if (!pedidos.Any()) return new List<ResponsePedidoDTO>();

            return MapearListaPedidosParaListaDto(pedidos);          
        }

        /// <inheritdoc/>
        public async Task<IList<ResponsePedidoDTO>> GetPedidosOrdenadosPorStatusEDataAsync()
        {
            var pedidos = await _pedidoRepository.GetPedidosOrdenadosPorStatusEDataAsync();
            return MapearListaPedidosParaListaDto(pedidos.ToList());
        }

        /// <inheritdoc/>
        public async Task<bool> AtualizarStatusPedido(int pedidoId, PedidoStatus status)
        {            
            var pedido = await _pedidoRepository.GetByIdAsync(pedidoId);

            if(pedido is null)
                throw new ValidationException("Pedido não encontrado");

            pedido.Status = status;

            await _pedidoRepository.UpdateAsync(pedido);

            return true;
        }

        private List<ResponsePedidoDTO> MapearListaPedidosParaListaDto(List<Pedido> pedidos)
        {
            var listResponsePedidoDTO = new List<ResponsePedidoDTO>();

            foreach (var pedido in pedidos)
            {
                var responsePedidoDTO = new ResponsePedidoDTO();
                responsePedidoDTO.Id = pedido.Id;
                responsePedidoDTO.Cliente = _mapper.Map<ResponseClienteDTO>(_clienteRepository.GetById(pedido.ClienteId));
                responsePedidoDTO.Status = pedido.Status.ToString();
                responsePedidoDTO.DataCriacao = pedido.DataCriacao;
                responsePedidoDTO.Valor = pedido.Valor;

                var itens = _pedidoItemRepository.GetByPedidoId(pedido.Id);

                if (itens is null || !itens.Any()) continue;

                responsePedidoDTO.Itens = new List<ResponsePedidoItemDTO>();

                foreach (var pedidoItem in itens)
                {
                    var responsePedidoItemDTO = new ResponsePedidoItemDTO();
                    var produtoDTO = _mapper.Map<ResponseProdutoDTO>(_produtoRepository.GetById(pedidoItem.ProdutoId));

                    responsePedidoItemDTO.Id = pedidoItem.Id;
                    responsePedidoItemDTO.Produto = produtoDTO;
                    responsePedidoItemDTO.Quantidade = pedidoItem.Quantidade;
                    responsePedidoItemDTO.Valor = pedidoItem.Valor;
                    responsePedidoItemDTO.DataCriacao = pedidoItem.DataCriacao;

                    responsePedidoDTO.Itens.Add(responsePedidoItemDTO);
                }

                listResponsePedidoDTO.Add(responsePedidoDTO);
            }

            return listResponsePedidoDTO;
        }
    }
}
