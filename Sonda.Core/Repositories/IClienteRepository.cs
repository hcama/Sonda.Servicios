using Sonda.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sonda.Core.Repositories
{
    public interface  IClienteRepository : IRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> getTodosClientes();
        Task<Cliente> getClienteId(int id);
    }
}
