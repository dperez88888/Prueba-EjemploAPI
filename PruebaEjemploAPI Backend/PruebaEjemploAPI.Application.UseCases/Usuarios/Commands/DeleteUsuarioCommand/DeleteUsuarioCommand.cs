using MediatR;
using PruebaEjemploAPI.Transversal.Common;

namespace PruebaEjemploAPI.Application.UseCases.Usuarios.Commands.DeleteUsuarioCommand
{
    public sealed record DeleteUsuarioCommand : IRequest<Response<bool>>
    {
        public int UsuarioId { get; set; }                
    }
}
