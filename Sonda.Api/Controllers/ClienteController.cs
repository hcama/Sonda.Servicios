using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sonda.Api.Resources;
using Sonda.Api.Validators;
using Sonda.Core.Models;
using Sonda.Core.Services;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Sonda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        // GET: /<controller>/
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteController(IClienteService clienteService, IMapper mapper)
        {
            this._mapper = mapper;
            this._clienteService = clienteService;
        }
        /// <summary>
        /// Muestra todos los  clientes.
        /// </summary>
        /// <returns>Todos los clientes</returns>   
        /// <response code="200">Retorna todos los tipos de clientes</response>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ClienteResource>>> getTodosClientes()
        {
            var clientes = await _clienteService.getTodosClientes();
            var clienteResources = _mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteResource>>(clientes);

            return Ok(clienteResources);
        }
        /// <summary>
        /// Muestra un cliente por id.
        /// </summary>
        /// <param name="id">Id del cliente a buscar</param>
        /// <returns>Muestra un clientes</returns>   
        /// <response code="200">Retorna un cliente</response>
        /// <response code="204">No se encontro el cliente</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteResource>> getClienteId(int id)
        {
            var cliente = await _clienteService.getClienteId(id);
            var clienteResource = _mapper.Map<Cliente, ClienteResource>(cliente);

            return Ok(clienteResource);
        }
        /// <summary>
        /// Inserta un cliente
        /// </summary>
        /// <param name="saveClienteResource">Datos del cliente a insertar</param>
        /// <returns>Muestra un clientes</returns>   
        /// <response code="200">Retorna el cliente insertado</response>
        /// <response code="204">No se encontro el cliente</response>
        /// <response code="400">Se encontraron errores</response>
        [HttpPost("")]
        public async Task<ActionResult<ClienteResource>> createCliente([FromBody] SaveClienteResource saveClienteResource)
        {
            var validator = new SaveClienteResourceValidator();
            var validationResult = await validator.ValidateAsync(saveClienteResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var clienteToCreate = _mapper.Map<SaveClienteResource, Cliente>(saveClienteResource);

            var newCliente = await _clienteService.createCliente(clienteToCreate);

            var cliente = await _clienteService.getClienteId(newCliente.Id);

            var clienteResource = _mapper.Map<Cliente, ClienteResource>(cliente);

            return Ok(clienteResource);
        }
        /// <summary>
        /// Modifica un cliente
        /// </summary>
        /// <param name="id">Id del cliente a modificar</param>
        /// <param name="saveClienteResource">Datos del cliente a modificar</param>
        /// <returns>Muestra un clientes</returns>   
        /// <response code="200">Retorna el cliente modificado</response>
        /// <response code="204">No se encontro el cliente, se realizo la transaccion</response>
        /// <response code="400">Se encontraron errores</response>
        /// <response code="404">No se encontro el cliente</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteResource>> updateCliente(int id, [FromBody] SaveClienteResource saveClienteResource)
        {
            var validator = new SaveClienteResourceValidator();
            var validationResult = await validator.ValidateAsync(saveClienteResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors);

            var clienteToBeUpdate = await _clienteService.getClienteId(id);

            if (clienteToBeUpdate == null)
                return NotFound();

            var cliente = _mapper.Map<SaveClienteResource, Cliente>(saveClienteResource);

            await _clienteService.updateCliente(clienteToBeUpdate, cliente);

            var updatedCliente = await _clienteService.getClienteId(id);
            var updatedClienteResource = _mapper.Map<Cliente, ClienteResource>(updatedCliente);

            return Ok(updatedClienteResource);
        }
        /// <summary>
        /// Elimina un cliente
        /// </summary>
        /// <param name="id">Id del cliente a eliminar</param>
        /// <returns>Muestra un clientes</returns>   
        /// <response code="200">Accion realizada</response>
        /// <response code="204">No se encontro el cliente, se realizo la transaccion</response>
        /// <response code="400">Se encontraron errores</response>
        /// <response code="404">No se encontro el cliente</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteCliente(int id)
        {
            if (id == 0)
                return BadRequest();

            var cliente = await _clienteService.getClienteId(id);

            if (cliente == null)
                return NotFound();

            await _clienteService.deleteCliente(cliente);

            return NoContent();
        }
    }
}
