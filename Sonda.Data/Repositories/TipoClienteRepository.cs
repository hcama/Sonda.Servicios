using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sonda.Core.Models;
using Sonda.Core.Repositories;

namespace Sonda.Data.Repositories
{
    public class TipoClienteRepository : Repository<TipoCliente>, ITipoClienteRepository
    {
        public TipoClienteRepository(AplicationDbContext context)
    : base(context)
        {

        }
        private AplicationDbContext AplicationDbContext
        {
            get { return Context as AplicationDbContext; }
        }

        public async Task<IEnumerable<TipoCliente>> getTodosTiposClientes()
        {
            return await AplicationDbContext.TipoClientes.ToListAsync();
        }
    }
}
