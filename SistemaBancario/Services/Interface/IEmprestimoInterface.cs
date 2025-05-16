using SistemaBancario.Models;

namespace SistemaBancario.Services.Interface
{
    public interface IEmprestimoInterface
    {

        Task<ResponseModel<Emprestimo>> SolicitarEmprestimo(decimal valor, int parcelas, double taxaJurosMensal, int clienteId, int contaBancariaId);
        Task<ResponseModel<bool>> QuitarEmprestimo(int emprestimoId);
        Task<ResponseModel<List<Emprestimo>>> ListarEmprestimosAtivos(int clienteId);
        Task<ResponseModel<Emprestimo>> ObterEmprestimoPorId(int emprestimoId);
    }
}
