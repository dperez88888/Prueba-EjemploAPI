using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PruebaExperticket_Backend.EntityConfig;
using PruebaExperticket_Backend.Model;

namespace PruebaExperticket_Backend.Context
{
    public class ContextDB : DbContext, IContextDB
    {

        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }

        public int SaveChanges(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
                
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ClienteEntityConfig.SetClienteEntityConfig(modelBuilder.Entity<Cliente>());            
        }

    }
}
