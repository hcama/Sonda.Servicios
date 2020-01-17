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
    }
}
