
using PruebaEjemploAPI.Application.Interface.Persistence;

namespace PruebaEjemploAPI.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IClienteRepository ClienteRepository { get; }

        public IUsuarioRepository UsuarioRepository { get; }

        public UnitOfWork(IClienteRepository clienteRepository, IUsuarioRepository usuarioRepository)
        {
            ClienteRepository = clienteRepository;
            UsuarioRepository = usuarioRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
