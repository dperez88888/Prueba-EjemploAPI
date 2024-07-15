using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaEjemploAPI_Backend.Model;

namespace PruebaEjemploAPI_Backend.EntityConfig
{
    public class ClienteEntityConfig
    {
        public static void SetClienteEntityConfig(EntityTypeBuilder<Cliente> entityBuilder)
        {
            entityBuilder.HasKey(x => x.ClienteId);
            entityBuilder.Property(x => x.Nombre).IsRequired();
            entityBuilder.Property(x => x.Nombre).HasMaxLength(100);
            entityBuilder.Property(x => x.Apellidos).IsRequired();
            entityBuilder.Property(x => x.Apellidos).HasMaxLength(200);
            entityBuilder.Property(x => x.Sexo).IsRequired();
            entityBuilder.Property(x => x.Sexo).HasMaxLength(1);
            entityBuilder.Property(x => x.FechaNacimiento).IsRequired();
            entityBuilder.Property(x => x.Pais).IsRequired();
            entityBuilder.Property(x=> x.Direccion).HasMaxLength(150);
            entityBuilder.Property(x => x.CodigoPostal).HasMaxLength(6);
            entityBuilder.Property(x => x.Email).HasMaxLength(50);
        }
    }
}
