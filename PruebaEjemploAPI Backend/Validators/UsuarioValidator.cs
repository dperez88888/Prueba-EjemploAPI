using FluentValidation;
using PruebaEjemploAPI_Backend.Aplicacion.DTO;
using Cst = PruebaEjemploAPI_Backend.Transversal.Common;

namespace PruebaEjemploAPI_Backend.Aplicacion.Validators
{
    public class UsuarioValidator : AbstractValidator<UsuarioDTO>        
    {

        public UsuarioValidator()
        {
            RuleFor(c => c.Nombre).NotNull().NotEmpty().MaximumLength(Cst.Constants.EMAIL_MAX_LENGTH);
            RuleFor(c => c.Apellidos).NotNull().NotEmpty().MaximumLength(Cst.Constants.APELLIDOSUSR_MAX_LENGTH);
            RuleFor(c => c.Password).NotNull().NotEmpty().MaximumLength(Cst.Constants.PASSWORD_MAX_LENGTH);
            RuleFor(c => c.Token).MaximumLength(Cst.Constants.TOKEN_MAX_LENGTH);
        }        
    }
}
