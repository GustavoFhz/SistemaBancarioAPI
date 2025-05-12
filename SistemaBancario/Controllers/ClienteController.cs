using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaBancario.Dto;
using SistemaBancario.Services.Interface;

namespace SistemaBancario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteIterface _clienteIterface;

        public ClienteController(IClienteIterface clienteIterface)
        {
            _clienteIterface = clienteIterface;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarCliente(ClienteCriacaoDto clienteCriacaoDto)
        {
            var cliente = await _clienteIterface.RegistrarCliente(clienteCriacaoDto);
            return Ok(cliente);
        }

        [HttpGet]
        public async Task<IActionResult> ListarClientes()
        {
            var clientes = await _clienteIterface.ListarClientes();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarClientePorId(int id)
        {
            var cliente = await _clienteIterface.BuscarPorId(id);
            return Ok(cliente);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletarCliente(int id)
        {
            var cliente = await _clienteIterface.RemoverCliente(id);
            return Ok(cliente); 
        }
        [HttpPut]
        public async Task<IActionResult> AlterarCliente(ClienteAlteracaoDto clienteAleracaoDto)
        {
            var cliente = await _clienteIterface.EditarCliente( clienteAleracaoDto );
            return Ok(cliente);
        }


    }
}
