using SistemaBancario.Models;

namespace SistemaBancario.Services.Interface
{
    public interface ICartaoInterface
    {
        Task<ResponseModel<Cartao>> Ativacao(int clienteId, bool ativo);
        Task<ResponseModel<Cartao>> Bloqueio(int clienteId);
        Task<ResponseModel<Cartao>> Vencimento(int clienteId);
    }
}
