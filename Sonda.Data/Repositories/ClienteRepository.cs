using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sonda.Core.Models;
using Sonda.Core.Repositories;

namespace Sonda.Data.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AplicationDbContext context)
            : base(context)
        {

        }
        private AplicationDbContext AplicationDbContext
        {
            get { return Context as AplicationDbContext; }
        }
        public async Task<IEnumerable<Cliente>> getTodosClientes()
        {
            return await AplicationDbContext.Clientes.ToListAsync();

        }

        public async Task<Cliente> getClienteId(int id)
        {
            return await AplicationDbContext.Clientes
            .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Cliente>> getTodosClientesbyTipoClienteId(int tipoClienteId)
        {
            return await AplicationDbContext.Clientes.
                Where(m => m.TipoClienteId == tipoClienteId)
                .ToListAsync();
        }
    }
}
