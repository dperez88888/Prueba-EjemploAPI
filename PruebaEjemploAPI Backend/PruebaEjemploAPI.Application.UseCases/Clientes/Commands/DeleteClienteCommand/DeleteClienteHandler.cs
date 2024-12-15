using AutoMapper;
using MediatR;
using PruebaEjemploAPI.Application.Interface.Persistence;
using PruebaEjemploAPI.Application.Validators;
using PruebaEjemploAPI.Domain.Entity;
using PruebaEjemploAPI.Transversal.Common;

namespace PruebaEjemploAPI.Application.UseCases.Clientes.Commands.DeleteClienteCommand
{
    public class DeleteClienteHandler : IRequestHandler<DeleteClienteCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteClienteHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
        {
            var res = new Response<bool>();

            res.Data = await _unitOfWork.ClienteRepository.DeleteClienteAsync(request.ClienteId);
            if (res.Data)
            {
                res.IsSuccess = true;
                res.Message = "Cliente Borrado con éxito";
            }

            return res;
        }
    }
}
