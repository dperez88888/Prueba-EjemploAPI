using MediatR;
using PruebaEjemploAPI.Application.DTO;
using PruebaEjemploAPI.Transversal.Common;

namespace PruebaEjemploAPI.Application.UseCases.Usuarios.Queries.AuthenticateQuery
{
    public sealed record AuthenticateQuery : IRequest<Response<UsuarioDTO>>
    {
        public string Nombre { get; set; }

        public string Password { get; set; }
    }
}
