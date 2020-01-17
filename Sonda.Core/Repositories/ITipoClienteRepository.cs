using Sonda.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sonda.Core.Repositories
{
    public interface ITipoClienteRepository : IRepository<TipoCliente>
    {
        Task<IEnumerable<TipoCliente>> getTodosTiposClientes();
    }
}
