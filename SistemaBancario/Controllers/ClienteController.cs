using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaBancario.Dto;
using SistemaBancario.Services;
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

        /// <summary>
        /// Registra um novo cliente.
        /// </summary>
        /// <param name="clienteCriacaoDto">Dados para criação do cliente.</param>
        /// <returns>Retorna os dados do cliente registrado.</returns>
        /// <response code="200">Cliente registrado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegistrarCliente([FromBody] ClienteCriacaoDto clienteCriacaoDto)
        {
            var cliente = await _clienteIterface.RegistrarCliente(clienteCriacaoDto);
            return Ok(cliente);
        }

        /// <summary>
        /// Faz o upload da foto do cliente identificado pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente para associar a foto.</param>
        /// <param name="foto">Arquivo de imagem a ser enviado.</param>
        /// <returns>Retorna a URL pública da imagem salva ou erro em caso de falha.</returns>
        [HttpPost("{id}/upload-foto")]

        public async Task<IActionResult> UploadFoto(int id, IFormFile foto)
        {
            try
            {
                var urlRelativa = await _clienteIterface.UploadFotoAsync(id, foto);
                var urlCompleta = $"{Request.Scheme}://{Request.Host}{urlRelativa}";
                return Ok(new { url = urlCompleta });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Lista todos os clientes cadastrados.
        /// </summary>
        /// <returns>Lista de clientes.</returns>
        /// <response code="200">Lista retornada com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarClientes()
        {
            var clientes = await _clienteIterface.ListarClientes();
            return Ok(clientes);
        }

        /// <summary>
        /// Busca um cliente pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente.</param>
        /// <returns>Dados do cliente encontrado.</returns>
        /// <response code="200">Cliente encontrado com sucesso.</response>
        /// <response code="404">Cliente não encontrado.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarClientePorId(int id)
        {
            var cliente = await _clienteIterface.BuscarPorId(id);
            return Ok(cliente);
        }

        /// <summary>
        /// Deleta um cliente pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente a ser deletado.</param>
        /// <returns>Confirmação da remoção.</returns>
        /// <response code="200">Cliente removido com sucesso.</response>
        /// <response code="404">Cliente não encontrado.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletarCliente(int id)
        {
            var cliente = await _clienteIterface.RemoverCliente(id);
            return Ok(cliente);
        }

        /// <summary>
        /// Altera os dados de um cliente.
        /// </summary>
        /// <param name="clienteAleracaoDto">Dados atualizados do cliente.</param>
        /// <returns>Dados do cliente após atualização.</returns>
        /// <response code="200">Cliente atualizado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="404">Cliente não encontrado.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AlterarCliente([FromBody] ClienteAlteracaoDto clienteAleracaoDto)
        {
            var cliente = await _clienteIterface.EditarCliente(clienteAleracaoDto);
            return Ok(cliente);
        }
        
    }

}
