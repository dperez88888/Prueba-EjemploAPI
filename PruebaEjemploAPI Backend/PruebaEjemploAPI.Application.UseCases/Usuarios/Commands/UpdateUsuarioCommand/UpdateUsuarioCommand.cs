using MediatR;
using PruebaEjemploAPI.Transversal.Common;

namespace PruebaEjemploAPI.Application.UseCases.Usuarios.Commands.AddUsuarioCommand
{
    public sealed record UpdateUsuarioCommand : IRequest<Response<bool>>
    {
        public int UsuarioId { get; set; }

        public required string Nombre { get; set; }

        public required string Apellidos { get; set; }

        public required string Password { get; set; }

        public string Token { get; set; }
    }
}
