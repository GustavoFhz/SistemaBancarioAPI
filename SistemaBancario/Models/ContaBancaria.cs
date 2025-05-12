using SistemaBancario.Enum;
using SistemaBancario.Models;

public class ContaBancaria
{
    public int Id { get; set; }
    public string? NumeroConta { get; set; }
    public string? Agencia { get; set; }
    public TipoContaEnum Tipo { get; set; }
    public double Saldo { get; set; }     
    public bool Ativa { get; set; }
    public DateTime DataCriacao { get; set; }
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }

    public ICollection<Transacao> Transacoes { get; set; }
    public ICollection<Transacao> TransacoesOrigem { get; set; }  
    public ICollection<Transacao> TransacoesDestino { get; set; }   
    public ICollection<Emprestimo> Emprestimos { get; set; }
}
