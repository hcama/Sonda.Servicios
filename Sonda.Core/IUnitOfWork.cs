using Sonda.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Sonda.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IClienteRepository Clientes { get; }
        ITipoClienteRepository TipoClientes { get; }
        Task<int> CommitAsync();
    }
}
