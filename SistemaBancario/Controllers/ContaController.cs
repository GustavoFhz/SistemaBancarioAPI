using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaBancario.Dto;
using SistemaBancario.Services.Interface;

namespace SistemaBancario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly IContaInterface _contaService;

        public ContaController(IContaInterface contaService)
        {
            _contaService = contaService;
        }

        /// <summary>
        /// Cria uma nova conta bancária.
        /// </summary>
        /// <param name="contaBancariaDto">Dados para abertura da conta.</param>
        /// <returns>Dados da conta criada.</returns>
        /// <response code="200">Conta criada com sucesso.</response>
        /// <response code="400">Dados inválidos para criação da conta.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarConta([FromBody] ContaBancariaDto contaBancariaDto)
        {
            var conta = await _contaService.AberturaConta(contaBancariaDto);
            return Ok(conta);
        }

        /// <summary>
        /// Consulta o saldo de uma conta bancária.
        /// </summary>
        /// <param name="numeroConta">Número da conta bancária.</param>
        /// <returns>Saldo atual da conta.</returns>
        /// <response code="200">Saldo consultado com sucesso.</response>
        /// <response code="404">Conta não encontrada.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConsultarSaldo([FromQuery] string numeroConta)
        {
            var saldo = await _contaService.ConsultaSaldo(numeroConta);
            return Ok(saldo);
        }
    }
}
