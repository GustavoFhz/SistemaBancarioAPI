using Microsoft.EntityFrameworkCore;
using SistemaBancario.Data;
using SistemaBancario.Models;
using SistemaBancario.Services.Interface;

public class CartaoServices : ICartaoInterface
{
    private readonly AppDbContext _context;
    public CartaoServices(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<Cartao>> Ativacao(int clienteId, bool ativo)
    {
        ResponseModel<Cartao> response = new ResponseModel<Cartao>();

        var cartao = await _context.Cartaos.FirstOrDefaultAsync(c => c.ClienteId == clienteId);

        try
        {
            if (cartao == null)
            {
                response.Mensagem = "Cartão não localizado";
                response.Status = false;
                return response;
            }

            cartao.Ativo = ativo;
            await _context.SaveChangesAsync();

            response.Dados = cartao;
            response.Mensagem = ativo ? "Cartão ativado com sucesso!" : "Cartão desativado com sucesso!";
            response.Status = true;
        }
        catch (Exception ex)
        {
            response.Mensagem = ex.Message;
            response.Status = false;
        }

        return response;
    }

    public async Task<ResponseModel<Cartao>> Bloqueio(int clienteId)
    {
        ResponseModel<Cartao> response = new ResponseModel<Cartao>();

        var cartao = await _context.Cartaos.FirstOrDefaultAsync(c => c.ClienteId == clienteId);

        try
        {
            if (cartao == null)
            {
                response.Mensagem = "Cartão não localizado";
                response.Status = false;
                return response;
            }

            cartao.Bloqueio = true;
            await _context.SaveChangesAsync();

            response.Dados = cartao;
            response.Mensagem = "Cartão bloqueado com sucesso!";
        }
        catch (Exception ex)
        {
            response.Mensagem = ex.Message;
            response.Status = false;
        }

        return response;
    }

    public async Task<ResponseModel<Cartao>> Vencimento(int clienteId)
    {
        ResponseModel<Cartao> response = new ResponseModel<Cartao>();

        var cartao = await _context.Cartaos.FirstOrDefaultAsync(c => c.ClienteId == clienteId);

        try
        {
            if (cartao == null)
            {
                response.Mensagem = "Cartão não localizado";
                response.Status = false;
                return response;
            }

            if (cartao.Vencimento < DateTime.Now)
            {
                response.Mensagem = "Cartão vencido.";
            }
            else
            {
                response.Mensagem = "Cartão ainda está válido.";
            }

            response.Dados = cartao;
        }
        catch (Exception ex)
        {
            response.Mensagem = ex.Message;
            response.Status = false;
        }

        return response;
    }
}
