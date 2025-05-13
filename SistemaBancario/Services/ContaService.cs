using Microsoft.EntityFrameworkCore;
using SistemaBancario.Data;
using SistemaBancario.Dto;
using SistemaBancario.Models;
using SistemaBancario.Services.Interface;

namespace SistemaBancario.Services
{
    public class ContaService : IContaInterface
    {
        private readonly AppDbContext _context;
        public ContaService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<ContaBancaria>> AberturaConta(ContaBancariaDto contaBancariaDto)
        {
            ResponseModel<ContaBancaria> response = new ResponseModel<ContaBancaria>();

            try
            {
                if (contaExistente(contaBancariaDto))
                {
                    response.Mensagem = "Conta bancária já existente";
                    response.Status = false;
                    return response;
                }
                var conta = new ContaBancaria
                {

                    NumeroConta = contaBancariaDto.NumeroConta,
                    Agencia = contaBancariaDto.Agencia,
                    Tipo = contaBancariaDto.Tipo,
                    Saldo = contaBancariaDto.Saldo,
                    ClienteId = contaBancariaDto.ClienteId
                };

                _context.ContasBancarias.Add(conta);
                await _context.SaveChangesAsync();

                response.Mensagem = "Cliente cadastrado com sucesso!";
                response.Dados = conta;
                response.Status = true;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
            return response;

        }

        public async Task<ResponseModel<ContaBancaria>> ConsultaSaldo(string numeroConta)
        {
            ResponseModel<ContaBancaria> response = new ResponseModel<ContaBancaria>();

            try
            {
                var conta = await _context.ContasBancarias
            .FirstOrDefaultAsync(c => c.NumeroConta == numeroConta);

                if(conta == null)
                {
                    response.Mensagem = "Conta não encontrada!";
                    response.Status = false;
                    return response;
                }
                response.Mensagem = $"Saldo atual: R$ {conta.Saldo:F2}";
                response.Dados = conta;
                return response;

            }
            catch(Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status= false;
                return response;
            }
        }

        public bool contaExistente(ContaBancariaDto contaBancariaDto)
        {
            return _context.ContasBancarias.Any(item => item.NumeroConta == contaBancariaDto.NumeroConta);
        }
    }
}
