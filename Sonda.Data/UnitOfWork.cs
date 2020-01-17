using Sonda.Core;
using Sonda.Core.Repositories;
using Sonda.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace Sonda.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AplicationDbContext _context;
        private IClienteRepository _clientesRepository;
        private ITipoClienteRepository _tipoClienteRepository;

        public UnitOfWork(AplicationDbContext context)
        {
            this._context = context;
        }

        public IClienteRepository Clientes => _clientesRepository = _clientesRepository ?? new ClienteRepository(_context);

        public ITipoClienteRepository TipoClientes => _tipoClienteRepository = _tipoClienteRepository ?? new TipoClienteRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
