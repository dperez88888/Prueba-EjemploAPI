using AutoMapper;
using PruebaEjemploAPI.Application.DTO;
using PruebaEjemploAPI.Application.UseCases.Clientes.Commands.AddClienteCommand;
using PruebaEjemploAPI.Application.UseCases.Clientes.Commands.UpdateClienteCommand;
using PruebaEjemploAPI.Application.UseCases.Usuarios.Commands.AddUsuarioCommand;
using PruebaEjemploAPI.Domain.Entity;

namespace PruebaEjemploAPI.Application.Common.Mapper
{
    // Clase que mapea las entidades
    public class EntityMapperProfile : Profile
    {
        public EntityMapperProfile()
        {                        
            CreateMap<ClienteDTO, Cliente>().ReverseMap();
            CreateMap<Task<Cliente>, Task<ClienteDTO>>().ReverseMap();
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();            
            CreateMap<Task<Usuario>, Task<UsuarioDTO>>().ReverseMap();
            CreateMap<Usuario, AddUsuarioCommand>().ReverseMap();
            CreateMap<UsuarioDTO, AddUsuarioCommand>().ReverseMap();
            CreateMap<Cliente, AddClienteCommand>().ReverseMap();
            CreateMap<ClienteDTO, AddClienteCommand>().ReverseMap();
            CreateMap<Usuario, UpdateUsuarioCommand>().ReverseMap();
            CreateMap<UsuarioDTO, UpdateUsuarioCommand>().ReverseMap();
            CreateMap<Cliente, UpdateClienteCommand>().ReverseMap();
            CreateMap<ClienteDTO, UpdateClienteCommand>().ReverseMap();
        }       
    }
}
