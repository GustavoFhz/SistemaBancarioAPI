using Microsoft.EntityFrameworkCore;
using SistemaBancario.Data;
using SistemaBancario.Models;
using SistemaBancario.Services.Interface;

namespace SistemaBancario.Services
{
    public class EmprestimoService : IEmprestimoInterface
    {
        private readonly AppDbContext _context;
        public EmprestimoService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<List<Emprestimo>>> ListarEmprestimosAtivos(int clienteId)
        {
            var response = new ResponseModel<List<Emprestimo>>();

            try
            {
                var emprestimos = await _context.Emprestimos
                    .Where(e => e.ClienteId == clienteId && e.Status)
                    .Include(e => e.Cliente)
                    .Include(e => e.ContaBancaria)
                    .ToListAsync();

                response.Dados = emprestimos;
                response.Status = true;
                response.Mensagem = "Empréstimos ativos listados com sucesso.";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<Emprestimo>> ObterEmprestimoPorId(int emprestimoId)
        {
            var response = new ResponseModel<Emprestimo>();

            try
            {
                var emprestimo = await _context.Emprestimos
                    .Include(e => e.Cliente)
                    .Include(e => e.ContaBancaria)
                    .FirstOrDefaultAsync(e => e.Id == emprestimoId);

                if (emprestimo == null)
                {
                    response.Status = false;
                    response.Mensagem = "Empréstimo não encontrado.";
                    return response;
                }

                response.Dados = emprestimo;
                response.Status = true;
                response.Mensagem = "Empréstimo localizado com sucesso.";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<bool>> QuitarEmprestimo(int emprestimoId)
        {
            {
                var response = new ResponseModel<bool>();

                try
                {
                    var emprestimo = await _context.Emprestimos.FindAsync(emprestimoId);

                    if (emprestimo == null)
                    {
                        response.Status = false;
                        response.Mensagem = "Empréstimo não encontrado.";
                        response.Dados = false;
                        return response;
                    }

                    if (!emprestimo.Status)
                    {
                        response.Status = false;
                        response.Mensagem = "Empréstimo já está quitado.";
                        response.Dados = false;
                        return response;
                    }

                    emprestimo.Status = false; // quitado
                    await _context.SaveChangesAsync();

                    response.Dados = true;
                    response.Status = true;
                    response.Mensagem = "Empréstimo quitado com sucesso.";
                    return response;
                }
                catch (Exception ex)
                {
                    response.Status = false;
                    response.Mensagem = ex.Message;
                    response.Dados = false;
                    return response;
                }
            }
        }

        public async Task<ResponseModel<Emprestimo>> SolicitarEmprestimo(decimal valor, int parcelas, double taxaJurosMensal, int clienteId, int contaBancariaId)
        {
            var response = new ResponseModel<Emprestimo>();

            try
            {
                // Verifica se cliente existe
                var clienteExiste = await _context.Clientes.AnyAsync(c => c.ClienteId == clienteId);
                if (!clienteExiste)
                {
                    response.Status = false;
                    response.Mensagem = "Cliente não encontrado.";
                    return response;
                }

                // Verifica se conta bancária existe e pertence ao cliente
                var contaExiste = await _context.ContasBancarias.AnyAsync(c => c.Id == contaBancariaId && c.ClienteId == clienteId);
                if (!contaExiste)
                {
                    response.Status = false;
                    response.Mensagem = "Conta bancária inválida ou não pertence ao cliente.";
                    return response;
                }

                var emprestimo = new Emprestimo
                {
                    Valor = valor,
                    Parcelas = parcelas,
                    TaxaJurosMensal = taxaJurosMensal,
                    DataContratacao = DateTime.Now,
                    Status = true,
                    ClienteId = clienteId,
                    ContaBancariaId = contaBancariaId
                };

                await _context.Emprestimos.AddAsync(emprestimo);
                await _context.SaveChangesAsync();

                response.Dados = emprestimo;
                response.Mensagem = "Empréstimo solicitado com sucesso!";
                response.Status = true;
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
