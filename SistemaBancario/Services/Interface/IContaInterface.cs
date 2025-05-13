using SistemaBancario.Dto;
using SistemaBancario.Models;

namespace SistemaBancario.Services.Interface
{
    public interface IContaInterface
    {
        Task<ResponseModel<ContaBancaria>>AberturaConta(ContaBancariaDto contaBancariaDto);
        Task<ResponseModel<ContaBancaria>> ConsultaSaldo(string numeroConta);
    }
}
