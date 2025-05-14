using SistemaBancario.Models;

namespace SistemaBancario.Services.Interface
{
    public interface ITransacaoInterface
    {
        Task<ResponseModel<Transacao>> Saque(int ContaId, decimal valor);
        Task<ResponseModel<Transacao>> Deposito(int ContaId, decimal valor);
        Task<ResponseModel<Transacao>> Transferencia(int contaOrigem, int ContaDestino, decimal valor);
    }
}
