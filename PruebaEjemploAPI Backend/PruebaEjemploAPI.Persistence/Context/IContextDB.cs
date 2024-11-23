using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PruebaEjemploAPI.Domain.Entity;

namespace PruebaEjemploAPI.Persistence.Context
{
    public interface IContextDB
    {
        DbSet<Cliente> Clientes { get; set; }

        DbSet<Usuario> Usuarios { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges(CancellationToken cancellationToken = default);
        void RemoveRange(IEnumerable<object> entities);
        EntityEntry Update(object entity);

    }
}
