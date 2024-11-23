using Microsoft.EntityFrameworkCore;
using PruebaEjemploAPI.Domain.Entity;
using PruebaEjemploAPI.Persistence.EntityConfig;

namespace PruebaEjemploAPI.Persistence.Context
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
