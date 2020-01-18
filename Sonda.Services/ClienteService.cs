using Sonda.Core;
using Sonda.Core.Models;
using Sonda.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sonda.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClienteService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Cliente>> getTodosClientes()
        {
            return await _unitOfWork.Clientes
                .getTodosClientes();
        }

        public async Task<Cliente> createCliente(Cliente newCliente)
        {
            await _unitOfWork.Clientes.AddAsync(newCliente);
            await _unitOfWork.CommitAsync();
            return newCliente;
        }

        public async Task deleteCliente(Cliente cliente)
        {
            _unitOfWork.Clientes.Remove(cliente);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Cliente> getClienteId(int id)
        {
            return await _unitOfWork.Clientes.getClienteId(id);
        }

        public async Task updateCliente(Cliente clienteToBeUpdated, Cliente cliente)
        {
            clienteToBeUpdated.Nombre = cliente.Nombre;
            clienteToBeUpdated.ApellidoPaterno = cliente.ApellidoPaterno;
            clienteToBeUpdated.ApellidoMaterno = cliente.ApellidoMaterno;
            clienteToBeUpdated.TipoClienteId = cliente.TipoClienteId;
            await _unitOfWork.CommitAsync();
        }
    }
}
