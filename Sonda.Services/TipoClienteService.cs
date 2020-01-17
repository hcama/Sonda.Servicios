using Sonda.Core;
using Sonda.Core.Models;
using Sonda.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sonda.Services
{
    public class TipoClienteService : ITipoClienteService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TipoClienteService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<TipoCliente>> getTodosTiposClientes()
        {
            return await _unitOfWork.TipoClientes.getTodosTiposClientes();          
        }
    }
}
