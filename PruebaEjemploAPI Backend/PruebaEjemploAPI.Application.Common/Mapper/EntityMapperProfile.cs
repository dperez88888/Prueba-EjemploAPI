using AutoMapper;
using PruebaEjemploAPI.Application.DTO;
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
                    }       
    }
}
