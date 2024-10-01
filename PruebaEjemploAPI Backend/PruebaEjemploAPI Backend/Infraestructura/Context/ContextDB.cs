using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PruebaEjemploAPI_Backend.Infraestructura.EntityConfig;
using PruebaEjemploAPI_Backend.Infraestructura.Model;

namespace PruebaEjemploAPI_Backend.Infraestructura.Context
{
    public class ContextDB : DbContext, IContextDB
    {

        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public int SaveChanges(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ClienteEntityConfig.SetClienteEntityConfig(modelBuilder.Entity<Cliente>());
            UsuarioEntityConfig.SetUsuarioEntityConfig(modelBuilder.Entity<Usuario>());
        }

    }
}
