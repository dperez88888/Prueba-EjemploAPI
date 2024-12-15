using AutoMapper;
using MediatR;
using PruebaEjemploAPI.Application.DTO;
using PruebaEjemploAPI.Application.Interface.Persistence;
using PruebaEjemploAPI.Application.Validators;
using PruebaEjemploAPI.Domain.Entity;
using PruebaEjemploAPI.Transversal.Common;

namespace PruebaEjemploAPI.Application.UseCases.Usuarios.Commands.DeleteUsuarioCommand
{
    public class DeleteUsuarioHandler : IRequestHandler<DeleteUsuarioCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUsuarioHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {
            var res = new Response<bool>();
                        
            res.Data = await _unitOfWork.UsuarioRepository.DeleteUsuarioAsync(request.UsuarioId);
            if (res.Data)
            {
                res.IsSuccess = true;
                res.Message = "Usuario Insertado con éxito";
            }            

            return res;
        }
    }
}
