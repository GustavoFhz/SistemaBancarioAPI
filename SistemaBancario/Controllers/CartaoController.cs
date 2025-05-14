using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaBancario.Services.Interface;

namespace SistemaBancario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartaoController : ControllerBase
    {
        private readonly ICartaoInterface _cartaoInterface;

        public CartaoController(ICartaoInterface cartaoInterface)
        {
            _cartaoInterface = cartaoInterface;
        }

        /// <summary>
        /// Ativa ou desativa o cartão de um cliente.
        /// </summary>
        /// <param name="clienteId">ID do cliente cujo cartão será ativado ou desativado.</param>
        /// <param name="ativo">Define se o cartão será ativado (true) ou desativado (false).</param>
        /// <returns>Retorna o cartão atualizado com o status de ativação.</returns>
        [HttpPut("ativacao")]
        public async Task<IActionResult> Ativacao(int clienteId, bool ativo)
        {
            var cartao = await _cartaoInterface.Ativacao(clienteId, ativo);
            return Ok(cartao);
        }

        /// <summary>
        /// Bloqueia o cartão de um cliente.
        /// </summary>
        /// <param name="clienteId">ID do cliente cujo cartão será bloqueado.</param>
        /// <returns>Retorna o cartão atualizado com o status de bloqueio.</returns>
        [HttpPut("bloqueio")]
        public async Task<IActionResult> Bloqueio(int clienteId)
        {
            var cartao = await _cartaoInterface.Bloqueio(clienteId);
            return Ok(cartao);
        }

        /// <summary>
        /// Verifica o vencimento do cartão de um cliente.
        /// </summary>
        /// <param name="clienteId">ID do cliente cujo cartão será verificado.</param>
        /// <returns>Retorna uma mensagem sobre a validade do cartão.</returns>
        [HttpGet("vencimento")]
        public async Task<IActionResult> Vencimento(int clienteId)
        {
            var cartao = await _cartaoInterface.Vencimento(clienteId);
            return Ok(cartao);
        }
    }
}
