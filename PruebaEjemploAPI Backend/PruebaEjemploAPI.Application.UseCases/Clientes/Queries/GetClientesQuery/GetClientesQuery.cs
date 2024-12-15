using MediatR;
using PruebaEjemploAPI.Application.DTO;
using PruebaEjemploAPI.Transversal.Common;

namespace PruebaEjemploAPI.Application.UseCases.Clientes.Queries.GetClientesQuery
{
    public sealed record GetClientesQuery : IRequest<Response<List<ClienteDTO>>>
    {
    }
}
