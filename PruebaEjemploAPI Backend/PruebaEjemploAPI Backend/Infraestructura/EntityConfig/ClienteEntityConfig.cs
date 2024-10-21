using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaEjemploAPI_Backend.Infraestructura.Model;
using Cst = PruebaEjemploAPI_Backend.Transversal.Common;

namespace PruebaEjemploAPI_Backend.Infraestructura.EntityConfig
{
    public class ClienteEntityConfig
    {
        public static void SetClienteEntityConfig(EntityTypeBuilder<Cliente> entityBuilder)
        {
            entityBuilder.HasKey(x => x.ClienteId);
            entityBuilder.Property(x => x.Nombre).IsRequired();
            entityBuilder.Property(x => x.Nombre).HasMaxLength(Cst.Constants.NOMBRE_MAX_LENGTH);
            entityBuilder.Property(x => x.Apellidos).IsRequired();
            entityBuilder.Property(x => x.Apellidos).HasMaxLength(Cst.Constants.APELLIDOS_MAX_LENGTH);
            entityBuilder.Property(x => x.Sexo).IsRequired();
            entityBuilder.Property(x => x.Sexo).HasMaxLength(Cst.Constants.SEXO_MAX_LENGTH);
            entityBuilder.Property(x => x.FechaNacimiento).IsRequired();
            entityBuilder.Property(x => x.Pais).IsRequired();
            entityBuilder.Property(x => x.Direccion).HasMaxLength(Cst.Constants.DIRECCION_MAX_LENGTH);
            entityBuilder.Property(x => x.CodigoPostal).HasMaxLength(Cst.Constants.CP_MAX_LENGTH);
            entityBuilder.Property(x => x.Email).HasMaxLength(Cst.Constants.EMAIL_MAX_LENGTH);
        }
    }
}
