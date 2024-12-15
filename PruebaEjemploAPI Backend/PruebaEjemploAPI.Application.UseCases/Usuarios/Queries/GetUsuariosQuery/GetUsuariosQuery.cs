using MediatR;
using PruebaEjemploAPI.Application.DTO;
using PruebaEjemploAPI.Transversal.Common;

namespace PruebaEjemploAPI.Application.UseCases.Usuarios.Queries.GetUsuariosQuery
{
    public sealed record GetUsuariosQuery : IRequest<Response<List<UsuarioDTO>>>
    {
    }
}
