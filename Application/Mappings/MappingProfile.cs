using AutoMapper;
using Domain.Entidades;
using Application.DTOs;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Cliente
            CreateMap<CreateClienteDTO, Cliente>();
            CreateMap<Cliente, ResponseClienteDTO>();
            //Produto
            CreateMap<CreateProdutoDTO, Produto>();
            CreateMap<Produto, CreateProdutoDTO>();
            CreateMap<Produto, ResponseProdutoDTO>();
            CreateMap<UpdateProdutoDTO, Produto>();
            CreateMap<Produto, UpdateProdutoDTO>();
        }
    }
}
