using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PruebaEjemploAPI_Backend.Infraestructura.Model;

namespace PruebaEjemploAPI_Backend.Infraestructura.Context
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
