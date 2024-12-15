using MediatR;
using PruebaEjemploAPI.Transversal.Common;

namespace PruebaEjemploAPI.Application.UseCases.Clientes.Commands.DeleteClienteCommand
{
    public sealed record DeleteClienteCommand : IRequest<Response<bool>>
    {
        public int ClienteId { get; set; }
    }
}
