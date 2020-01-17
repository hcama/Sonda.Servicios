using Sonda.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sonda.Core.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> getTodosClientes();
    }
}
