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
        private readonly IContaInterface _contaInterface;

        public ContaController(IContaInterface contaInterface)
        {
            _contaInterface = contaInterface;
        }


        [HttpPost]
        public async Task<IActionResult> CriarConta(ContaBancariaDto contaBancariaDto)
        {
            var conta = await _contaInterface.AberturaConta(contaBancariaDto);
            return Ok(conta);
        }

        [HttpGet]

        public async Task<IActionResult> ConsultarSaldo(string numeroConta)
        {
            var saldo = await _contaInterface.ConsultaSaldo(numeroConta);
            return Ok(saldo);
        }
    }
}
