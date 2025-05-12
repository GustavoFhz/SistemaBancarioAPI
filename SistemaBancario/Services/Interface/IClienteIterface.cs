using SistemaBancario.Dto;
using SistemaBancario.Models;

namespace SistemaBancario.Services.Interface
{
    public interface IClienteIterface
    {
        Task<ResponseModel<Cliente>> RegistrarCliente(ClienteCriacaoDto clienteCriacaoDto);
        Task<ResponseModel<List<Cliente>>> ListarClientes();
        Task<ResponseModel<Cliente>> BuscarPorId(int id);
        Task<ResponseModel<Cliente>> EditarCliente(ClienteAlteracaoDto clienteAlteracaoDto);
        Task<ResponseModel<Cliente>> RemoverCliente(int id);
    }
}
