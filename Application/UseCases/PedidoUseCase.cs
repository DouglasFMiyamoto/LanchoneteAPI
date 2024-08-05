using Application.DTOs;
using Application.Repository;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.UseCases
{
    public class PedidoUseCase : IPedidoUseCase
    {
        public readonly IPedidoRepository _pedidoRepository;
        public readonly IClienteRepository _clienteRepository;
        public readonly IProdutoRepository _produtoRepository;
        public readonly IPedidoItemRepository _pedidoItemRepository;
        private IMapper _mapper;

        public PedidoUseCase(IPedidoRepository pedidoRepository,
                             IClienteRepository clienteRepository,
                             IProdutoRepository produtoRepository,
                             IPedidoItemRepository pedidoItemRepository,
                             IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
            _pedidoItemRepository = pedidoItemRepository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public void Save(CreatePedidoDTO pedidoDTO)
        {
            if (!IsValidCliente(pedidoDTO.ClienteId))
                throw new ValidationException("Cliente não encontrado");

            if (pedidoDTO.Itens is null || !pedidoDTO.Itens.Any())
                throw new ValidationException("O pedido não contém nenhum item");

            var itens = SetPedidoItems(pedidoDTO);

            if (!itens.Any()) throw new ValidationException("O pedido não contém nenhum item válido");

            var pedido = new Pedido
            {
                ClienteId = pedidoDTO.ClienteId,
                Status = PedidoStauts.Recebido,
                Itens = itens,
                DataCriacao = DateTime.Now,
                Valor = itens.Sum(i => i.Valor),
            };

            _pedidoRepository.Save(pedido);
        }

        /// <inheritdoc/>
        public IList<ResponsePedidoDTO> GetAll()
        {
            List<Pedido> pedidos = _pedidoRepository.GetAll()?.OrderBy(x => x.DataCriacao).ToList() ?? new List<Pedido>();

            if (!pedidos.Any()) return new List<ResponsePedidoDTO>();

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

                foreach(var pedidoItem in itens) 
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

        private List<PedidoItem> SetPedidoItems(CreatePedidoDTO pedidoDTO)
        {

            var retorno = new List<PedidoItem>();

            if (pedidoDTO.Itens is null || !pedidoDTO.Itens.Any()) return retorno;

            foreach (var item in pedidoDTO.Itens)
            {
                var produto = _produtoRepository.GetById(item.ProdutoId);

                if (produto is null || item.Quantidade <= 0) continue;

                var pedidoItem = new PedidoItem
                {
                    ProdutoId = produto.Id,
                    Customizacao = item.Customizacao,
                    Quantidade = item.Quantidade,
                    Valor = produto.Valor * item.Quantidade,
                    DataCriacao = DateTime.Now
                };

                retorno.Add(pedidoItem);
            }

            return retorno;
        }

        private bool IsValidCliente(int clienteId)
        {
            var cliente = _clienteRepository.GetById(clienteId);
            return cliente != null;
        }
    }
}
