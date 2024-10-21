using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;
using PruebaEjemploAPI_Backend.Infraestructura.Model;
using Cst = PruebaEjemploAPI_Backend.Transversal.Common;

namespace PruebaEjemploAPI_Backend.Infraestructura.EntityConfig
{
    public class UsuarioEntityConfig
    {
        public static void SetUsuarioEntityConfig(EntityTypeBuilder<Usuario> entityBuilder)
        {
            entityBuilder.HasKey(x => x.UsuarioId);
            entityBuilder.Property(x => x.Nombre).IsRequired();
            entityBuilder.Property(x => x.Nombre).HasMaxLength(Cst.Constants.NOMBRE_MAX_LENGTH);
            entityBuilder.Property(x => x.Apellidos).IsRequired();
            entityBuilder.Property(x => x.Apellidos).HasMaxLength(Cst.Constants.APELLIDOSUSR_MAX_LENGTH);
            entityBuilder.Property(x => x.Password).IsRequired();
            entityBuilder.Property(x => x.Password).HasMaxLength(Cst.Constants.PASSWORD_MAX_LENGTH);  
            entityBuilder.Property(x => x.Token).HasMaxLength(Cst.Constants.TOKEN_MAX_LENGTH);
        }
    }
}
