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
        Task<Cliente> getClienteId(int id);
        Task<Cliente> createCliente(Cliente newCliente);
        Task updateCliente(Cliente clienteToBeUpdated, Cliente cliente);
        Task deleteCliente(Cliente cliente);
    }
}
