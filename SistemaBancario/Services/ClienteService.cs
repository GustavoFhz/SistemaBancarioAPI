using Microsoft.EntityFrameworkCore;
using SistemaBancario.Data;
using SistemaBancario.Dto;
using SistemaBancario.Models;
using SistemaBancario.Services.Interface;

namespace SistemaBancario.Services
{
    public class ClienteService : IClienteIterface
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public ClienteService(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<ResponseModel<Cliente>> BuscarPorId(int id)
        {
            ResponseModel<Cliente> response = new ResponseModel<Cliente>();

            try
            {
                var cliente = await _context.Clientes.FindAsync(id);

                if(cliente == null)
                {
                    response.Mensagem = "Cliente não encontrado";
                    return response;
                }
                response.Dados = cliente;
                response.Mensagem = "Cliente localizado!";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<Cliente>> EditarCliente(ClienteAlteracaoDto clienteAlteracaoDto)
        {
            ResponseModel<Cliente> response = new ResponseModel<Cliente>();

            try
            {
                var clienteBanco = await _context.Clientes.FindAsync(clienteAlteracaoDto.ClienteId);

                if(clienteBanco == null)
                {
                    response.Mensagem = "Cliente não localizado";
                    return response;
                }

                clienteBanco.Nome = clienteAlteracaoDto.Nome;
                clienteBanco.Email = clienteAlteracaoDto.Email;
                clienteBanco.Telefone = clienteAlteracaoDto.Telefone;
                clienteBanco.Endereco = clienteAlteracaoDto.Endereco;

                _context.Update(clienteBanco);
                await _context.SaveChangesAsync();

                response.Mensagem = "Cliente alterado com sucesso";
                response.Dados = clienteBanco;
                return response;

            }
            catch(Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<Cliente>>> ListarClientes()
        {
            ResponseModel<List<Cliente>> response = new ResponseModel<List<Cliente>>();

            try
            {
                var clientes  = await _context.Clientes.ToListAsync();

                response.Dados = clientes;
                response.Mensagem = "Clientes localizados!";
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<Cliente>> RegistrarCliente(ClienteCriacaoDto clienteCriacaoDto)
        {
            ResponseModel<Cliente> response = new ResponseModel<Cliente>();

            try
            {
                if (ClienteExiste(clienteCriacaoDto))
                {
                    response.Mensagem = "Cliente já cadastrado";
                    response.Status = false;
                    return response;
                }

                var cliente = new Cliente
                {
                    Nome = clienteCriacaoDto.Nome,
                    CPF = clienteCriacaoDto.CPF,
                    Email = clienteCriacaoDto.Email,
                    Telefone = clienteCriacaoDto.Telefone,
                    DataNascimento = clienteCriacaoDto.DataNascimento,
                    Endereco = clienteCriacaoDto.Endereco
                };

                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                response.Mensagem = "Cliente registrado com sucesso";
                response.Dados = cliente;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
            }

            return response;
        }

        public async Task<ResponseModel<Cliente>> RemoverCliente(int id)
        {
            ResponseModel<Cliente> response = new ResponseModel<Cliente>();

            try
            {
                var cliente = await _context.Clientes.FindAsync(id);

                if(cliente == null)
                {
                    response.Mensagem = "Cliente não localizado!";
                    return response;
                }
                response.Dados = cliente;
                response.Mensagem = "Cliente removido com sucesso!";

                _context.Remove(cliente);
                await _context.SaveChangesAsync();
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public bool ClienteExiste(ClienteCriacaoDto clienteCriacaoDto)
        {
            return _context.Clientes.Any(item => item.Nome ==  clienteCriacaoDto.Nome );
        }

        public async Task<string> UploadFotoAsync(int id, IFormFile foto)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                throw new Exception("Cliente não encontrado.");

            if (foto == null || foto.Length == 0)
                throw new Exception("Nenhuma foto enviada.");

            var nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
            var pasta = Path.Combine(_environment.WebRootPath ?? "wwwroot", "images");
            var caminho = Path.Combine(pasta, nomeArquivo);

            Directory.CreateDirectory(pasta); 

            using (var stream = new FileStream(caminho, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }

            var urlDaImagem = $"/images/{nomeArquivo}";
            cliente.FotoUrl = urlDaImagem;

            await _context.SaveChangesAsync();

            return urlDaImagem;
        }
    }
}

