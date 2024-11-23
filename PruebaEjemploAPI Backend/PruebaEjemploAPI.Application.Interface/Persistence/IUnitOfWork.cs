
namespace PruebaEjemploAPI.Application.Interface.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IClienteRepository ClienteRepository { get; }

        IUsuarioRepository UsuarioRepository { get; }

    }
}
