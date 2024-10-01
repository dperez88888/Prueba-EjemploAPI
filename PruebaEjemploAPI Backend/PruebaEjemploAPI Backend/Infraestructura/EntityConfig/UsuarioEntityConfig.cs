using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaEjemploAPI_Backend.Infraestructura.Model;

namespace PruebaEjemploAPI_Backend.Infraestructura.EntityConfig
{
    public class UsuarioEntityConfig
    {
        public static void SetUsuarioEntityConfig(EntityTypeBuilder<Usuario> entityBuilder)
        {
            entityBuilder.HasKey(x => x.UsuarioId);
            entityBuilder.Property(x => x.Nombre).IsRequired();
            entityBuilder.Property(x => x.Nombre).HasMaxLength(100);
            entityBuilder.Property(x => x.Apellidos).IsRequired();
            entityBuilder.Property(x => x.Apellidos).HasMaxLength(200);
            entityBuilder.Property(x => x.Password).IsRequired();
            entityBuilder.Property(x => x.Password).HasMaxLength(20);  
            entityBuilder.Property(x => x.Token).HasMaxLength(500);
        }
    }
}
