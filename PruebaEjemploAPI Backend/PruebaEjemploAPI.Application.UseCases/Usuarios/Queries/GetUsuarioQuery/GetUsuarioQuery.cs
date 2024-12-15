using MediatR;
using PruebaEjemploAPI.Application.DTO;
using PruebaEjemploAPI.Transversal.Common;

namespace PruebaEjemploAPI.Application.UseCases.Usuarios.Queries.GetUsuarioQuery
{
    public sealed record GetUsuarioQuery : IRequest<Response<UsuarioDTO>>
    {
        public int UsuarioId { get; set; }
    }
}
