using SistemaBancario.Data;
using SistemaBancario.Enum;
using SistemaBancario.Models;
using SistemaBancario.Services.Interface;

namespace SistemaBancario.Services
{
    public class TransacaoService : ITransacaoInterface
    {
        private readonly AppDbContext _context;
        public TransacaoService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<Transacao>> Deposito(int ContaId, decimal valor)
        {
            ResponseModel<Transacao> response = new ResponseModel<Transacao>();

            try
            {
                var conta = await _context.ContasBancarias.FindAsync(ContaId);

                if (conta == null)
                {
                    response.Mensagem = "Conta não localizada";
                    response.Status = false;
                    return response;
                }

                conta.Saldo += valor;
                _context.ContasBancarias.Update(conta);

                var transacao = new Transacao
                {
                    ContaId = ContaId,
                    Tipo = TipoTransacaoEnum.deposito,
                    Valor = valor,
                    DataTransacao = DateTime.Now
                };

                await _context.Transacoes.AddAsync(transacao);
                await _context.SaveChangesAsync();

                response.Status = true;
                response.Mensagem = "Depósito realizado com sucesso";
                response.Dados = transacao;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro ao realizar depósito: {ex.Message}";
                return response;
            }
        }

        public async Task<ResponseModel<Transacao>> Saque(int ContaId, decimal valor)
        {
            var response = new ResponseModel<Transacao>();

            try
            {
                var conta = await _context.ContasBancarias.FindAsync(ContaId);

                if (conta == null)
                {
                    response.Mensagem = "Conta não localizada";
                    response.Status = false;
                    return response;
                }

                if (conta.Saldo < valor)
                {
                    response.Mensagem = "Saldo insuficiente";
                    response.Status = false;
                    return response;
                }

                conta.Saldo -= valor;
                _context.ContasBancarias.Update(conta);

                var transacao = new Transacao
                {
                    ContaId = ContaId,
                    Tipo = TipoTransacaoEnum.saque,
                    Valor = valor,
                    DataTransacao = DateTime.Now
                };

                await _context.Transacoes.AddAsync(transacao);
                await _context.SaveChangesAsync();

                response.Status = true;
                response.Mensagem = "Saque realizado com sucesso";
                response.Dados = transacao;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = $"Erro ao realizar saque: {ex.Message}";
                return response;
            }
        }


        public async Task<ResponseModel<Transacao>> Transferencia(int contaOrigem, int contaDestino, decimal valor)
        {
            var response = new ResponseModel<Transacao>();

            try
            {
                var origem = await _context.ContasBancarias.FindAsync(contaOrigem);
                var destino = await _context.ContasBancarias.FindAsync(contaDestino);

                if (origem == null || destino == null)
                {
                    response.Status = false;
                    response.Mensagem = "Conta de origem ou destino não localizada";
                    return response;
                }

                if (origem.Saldo < valor)
                {
                    response.Status = false;
                    response.Mensagem = "Saldo insuficiente na conta de origem";
                    return response;
                }

                origem.Saldo -= valor;
                destino.Saldo += valor;

                _context.ContasBancarias.Update(origem);
                _context.ContasBancarias.Update(destino);

                var transacao = new Transacao
                {
                    ContaId = contaOrigem,
                    ContaDestinoId = contaDestino,
                    Tipo = TipoTransacaoEnum.transferencia,
                    Valor = valor,
                    DataTransacao = DateTime.Now
                };

                await _context.Transacoes.AddAsync(transacao);
                await _context.SaveChangesAsync();

                response.Status = true;
                response.Mensagem = "Transferência realizada com sucesso";
                response.Dados = transacao;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }



    }
}
    



