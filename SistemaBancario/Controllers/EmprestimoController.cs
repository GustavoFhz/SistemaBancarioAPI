using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaBancario.Dto;
using SistemaBancario.Services.Interface;
using System.Threading.Tasks;

namespace SistemaBancario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimoController : ControllerBase
    {
        private readonly IEmprestimoInterface _empretimoService;

        public EmprestimoController(IEmprestimoInterface emprestimoService)
        {
            _empretimoService = emprestimoService;
        }

        /// <summary>
        /// Lista todos os empréstimos ativos de um cliente.
        /// </summary>
        /// <param name="clienteId">ID do cliente.</param>
        /// <returns>Lista de empréstimos ativos.</returns>
        /// <response code="200">Empréstimos listados com sucesso.</response>
        /// <response code="400">Erro na solicitação.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpGet("ativos/{clienteId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarEmprestimosAtivos(int clienteId)
        {
            var response = await _empretimoService.ListarEmprestimosAtivos(clienteId);
            if (response.Status)
                return Ok(response);
            return BadRequest(response);
        }

        /// <summary>
        /// Obtém um empréstimo pelo ID.
        /// </summary>
        /// <param name="emprestimoId">ID do empréstimo.</param>
        /// <returns>Detalhes do empréstimo.</returns>
        /// <response code="200">Empréstimo encontrado.</response>
        /// <response code="404">Empréstimo não encontrado.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpGet("{emprestimoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterEmprestimoPorId(int emprestimoId)
        {
            var response = await _empretimoService.ObterEmprestimoPorId(emprestimoId);
            if (response.Status)
                return Ok(response);
            return NotFound(response);
        }

        /// <summary>
        /// Quita um empréstimo ativo.
        /// </summary>
        /// <param name="emprestimoId">ID do empréstimo a ser quitado.</param>
        /// <returns>Resultado da operação de quitação.</returns>
        /// <response code="200">Empréstimo quitado com sucesso.</response>
        /// <response code="400">Empréstimo não encontrado ou já quitado.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpPut("quitar/{emprestimoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> QuitarEmprestimo(int emprestimoId)
        {
            var response = await _empretimoService.QuitarEmprestimo(emprestimoId);
            if (response.Status)
                return Ok(response);
            return BadRequest(response);
        }

        /// <summary>
        /// Solicita um novo empréstimo.
        /// </summary>
        /// <param name="request">Dados para solicitação do empréstimo.</param>
        /// <returns>Dados do empréstimo criado.</returns>
        /// <response code="201">Empréstimo solicitado com sucesso.</response>
        /// <response code="400">Dados inválidos ou cliente/conta não encontrados.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpPost("solicitar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SolicitarEmprestimo([FromBody] EmprestimoRequestDto request)
        {
            var response = await _empretimoService.SolicitarEmprestimo(
                request.Valor,
                request.Parcelas,
                request.TaxaJurosMensal,
                request.ClienteId,
                request.ContaBancariaId);

            if (response.Status)
                return CreatedAtAction(nameof(ObterEmprestimoPorId), new { emprestimoId = response.Dados.Id }, response);

            return BadRequest(response);
        }
    }

    // DTO para solicitação de empréstimo
    public class EmprestimoRequestDto
    {
        public decimal Valor { get; set; }
        public int Parcelas { get; set; }
        public double TaxaJurosMensal { get; set; }
        public int ClienteId { get; set; }
        public int ContaBancariaId { get; set; }
    }
}
