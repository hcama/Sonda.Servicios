using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sonda.Api.Resources;
using Sonda.Core.Models;
using Sonda.Core.Services;

namespace Sonda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class TipoClienteController : Controller
    {
        private readonly ITipoClienteService _tipoClienteService;
        private readonly IMapper _mapper;

        public TipoClienteController(ITipoClienteService tipoClienteService, IMapper mapper)
        {
            this._mapper = mapper;
            this._tipoClienteService = tipoClienteService;
        }
        /// <summary>
        /// Muestra todos los tipos de clientes.
        /// </summary>
        /// <returns>Todos los tipos de clientes</returns>   
        /// <response code="200">Retorna todos los tipos de clientes</response>
        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<TipoClienteResource>) ,StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TipoClienteResource>>> getTodosTiposClientes()
        {
            var tipoClientes = await _tipoClienteService.getTodosTiposClientes();
            var tipoClienteResources = _mapper.Map<IEnumerable<TipoCliente>, IEnumerable<TipoClienteResource>>(tipoClientes);

            return Ok(tipoClienteResources);
        }

    }
}