using MediatR;
using PruebaEjemploAPI.Application.DTO;
using PruebaEjemploAPI.Transversal.Common;

namespace PruebaEjemploAPI.Application.UseCases.Clientes.Queries.GetClienteQuery
{
    public sealed record GetClienteQuery : IRequest<Response<ClienteDTO>>
    {
        public int ClienteId { get; set; }
    }
}
