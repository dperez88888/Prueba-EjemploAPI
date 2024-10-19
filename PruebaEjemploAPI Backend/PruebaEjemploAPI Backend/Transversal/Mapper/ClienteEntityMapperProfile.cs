using AutoMapper;
using PruebaEjemploAPI_Backend.Infraestructura.Model;
using PruebaEjemploAPI_Backend.Aplicacion.DTO;
using PruebaEjemploAPI_Backend.Dominio.DTO;

namespace PruebaEjemploAPI_Backend.Transversal.Mapper
{
    // Clase que mapea la clase cliente
    public class ClienteEntityMapperProfile : Profile
    {
        public ClienteEntityMapperProfile()
        {
            CreateMap<Cliente, ClienteDomDTO>().ReverseMap();                   
            CreateMap<ClienteDomDTO, ClienteDTO>().ReverseMap();

            CreateMap<Task<Cliente>, Task<ClienteDomDTO>>().ReverseMap();
            CreateMap<Task<ClienteDomDTO>, Task<ClienteDTO>>().ReverseMap();

            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<UsuarioDTO, UsuarioDTO>().ReverseMap();

            CreateMap<Task<Usuario>, Task<UsuarioDTO>>().ReverseMap();
            CreateMap<Task<UsuarioDTO>, Task<UsuarioDTO>>().ReverseMap();
        }       
    }
}
